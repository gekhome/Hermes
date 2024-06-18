using System;
using System.Collections.Generic;
using System.Linq;
using Hermes.DAL;
using Hermes.Models;

namespace Hermes.BPM
{
    public static class Kerberos
    {
        public const int TICKET_TIMEOUT_MINUTES = 240;

        public static int WorkingDays(DateTime initial_date, DateTime final_date)
        {
            int daycount = 0;

            DateTime date1 = initial_date;
            DateTime date2 = final_date;

            while (date1 <= date2)
            {
                switch (date1.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                    case DayOfWeek.Saturday:
                        break;
                    case DayOfWeek.Monday:
                    case DayOfWeek.Tuesday:
                    case DayOfWeek.Wednesday:
                    case DayOfWeek.Thursday:
                    case DayOfWeek.Friday:
                        daycount++;
                        break;
                    default:
                        break;
                }
                date1 = date1.AddDays(1);
            }
            return daycount;
        }


        #region AFM validation

        /// ------------------------------------------------------------------------
        /// CheckAFM: Ελέγχει αν ένα ΑΦΜ είναι σωστό
        /// Το ΑΦΜ που θα ελέγξουμε
        /// true = ΑΦΜ σωστό, false = ΑΦΜ Λάθος
        /// Προσθήκη: Αποκλεισμός όταν όλα τα ψηφία = 0 (ο αλγόριθμος τα δέχεται!)
        /// Ημ/νια: 12/3/2013
        /// ------------------------------------------------------------------------
        public static bool CheckAFM(string cAfm)
        {
            int nExp = 1;
            // Ελεγχος αν περιλαμβάνει μόνο γράμματα
            try { long nAfm = Convert.ToInt64(cAfm); }

            catch (Exception) { return false; }

            // Ελεγχος μήκους ΑΦΜ
            if (string.IsNullOrWhiteSpace(cAfm))
            {
                return false;
            }

            cAfm = cAfm.Trim();
            int nL = cAfm.Length;
            if (nL != 9) return false;

            // Έλεγχος αν όλα τα ψηφία είναι 0
            var count = cAfm.Count(x => x == '0');
            if (count == cAfm.Length) return false;

            //Υπολογισμός αν το ΑΦΜ είναι σωστό

            int nSum = 0;
            int xDigit = 0;
            int nT = 0;

            for (int i = nL - 2; i >= 0; i--)
            {
                xDigit = int.Parse(cAfm.Substring(i, 1));
                nT = xDigit * (int)(Math.Pow(2, nExp));
                nSum += nT;
                nExp++;
            }

            xDigit = int.Parse(cAfm.Substring(nL - 1, 1));

            nT = nSum / 11;
            int k = nT * 11;
            k = nSum - k;
            if (k == 10) k = 0;
            if (xDigit != k) return false;

            return true;

        }

        #endregion


        #region ΓΕΝΙΚΟΙ ΕΛΕΓΧΟΙ

        public static bool ChildrenExist(int prosklisiId) 
        {
            using (var db = new HermesDBEntities())
            {
                var count = (from d in db.sqlCHILDREN_POPULATOR
                             where d.PROSKLISI_ID == prosklisiId
                             orderby d.FULLNAME
                             select d).Count();
                if (count > 0)
                    return true;
                else
                    return false;
            }
        }

        public static bool ChildrenStationExist(int prosklisiId, int stationId)
        {
            using (var db = new HermesDBEntities())
            {
                var count = (from d in db.sqlCHILDREN_POPULATOR
                             where d.PROSKLISI_ID == prosklisiId && d.STATION_ID == stationId
                             orderby d.FULLNAME
                             select d).Count();
                if (count > 0)
                    return true;
                else
                    return false;
            }
        }

        public static bool ParentExists(int userId)
        {
            using (var db = new HermesDBEntities())
            {
                var data = (from d in db.PARENTS where d.PARENT_USERID == userId select d).FirstOrDefault();
                if (data == null)
                    return false;
                return true;
            }
        }

        public static bool StatementExists(int userId)
        {
            int prosklisiId = Common.GetOpenProsklisiID();
            int parentsId = Common.GetParentIdFromUserId(userId);

            using (var db = new HermesDBEntities())
            {
                var data = (from d in db.STATEMENTS where d.PARENTS_ID == parentsId && d.PROSKLISI_ID == prosklisiId select d).FirstOrDefault();
                if (data == null)
                    return false;
                return true;
            }
        }

        public static bool existsUsername(string username)
        {
            using (var db = new HermesDBEntities())
            {
                var userParents = db.USER_PARENTS.Where(u => u.USERNAME == username).FirstOrDefault();
                var userAdmins = db.USER_ADMINS.Where(u => u.USERNAME == username).FirstOrDefault(); ;
                var userSchools = db.USER_STATIONS.Where(u => u.USERNAME == username).FirstOrDefault();

                return (userParents != null || userAdmins != null || userSchools != null);
            }
        }

        public static int CountSpaces(string s)
        {
            int countSpaces = s.Count(char.IsWhiteSpace);

            return countSpaces;
        }

        public static string ValidateParentsFields(ParentsViewModel p)
        {
            string errMsg = "";

            if (p.FAMILY_STATUS > 0 && !(p.CHILD_EPIMELEIA > 0))
            {
                errMsg += "->Για την οικογ. κατάσταση που επιλέξατε πρέπει να καταχωρήσετε επιμέλεια παιδιών.";
            }

            if (string.IsNullOrEmpty(p.FATHER_AFM) || string.IsNullOrEmpty(p.MOTHER_AFM))
            {
                errMsg += "->Τα Α.Φ.Μ. και των δύο γονέων είναι υποχρεωτικά πεδία.";
            }

            if (string.IsNullOrEmpty(p.FATHER_ADT) && string.IsNullOrEmpty(p.FATHER_PASSPORT))
            {
                errMsg += "->Πρέπει να συμπληρωθεί ΑΔΤ ή Αρ. διαβατηρίου για τον πατέρα.";
            }

            if (string.IsNullOrEmpty(p.MOTHER_ADT) && string.IsNullOrEmpty(p.MOTHER_PASSPORT))
            {
                errMsg += "->Πρέπει να συμπληρωθεί ΑΔΤ ή Αρ. διαβατηρίου για την μητέρα.";
            }
            if (!CheckAFM(p.FATHER_AFM))
            {
                errMsg += "->To Α.Φ.Μ. του πατέρα δεν είναι έγκυρο.";
            }
            if (!CheckAFM(p.MOTHER_AFM))
            {
                errMsg += "->->To Α.Φ.Μ. της μητέρας δεν είναι έγκυρο.";
            }
            return errMsg;
        }

        public static bool ValidBirthDate(DateTime birthdate)
        {
            bool result = true;
            int maxAge = 5;
            int minAge = 0;

            DateTime minDate = DateTime.Today.Date.AddYears(-maxAge);
            DateTime maxDate = DateTime.Today.Date.AddYears(-minAge);

            if (birthdate >= minDate && birthdate <= maxDate)
                result = true;
            else
                result = false;
            return result;
        }

        public static bool ValidateSameStations(int parentsId, int stationId, bool isnew = false)
        {
            int prosklisiId = Common.GetOpenProsklisiID();

            using (var db = new HermesDBEntities())
            {
                var data = (from d in db.AITISIS where d.PROSKLISI_ID == prosklisiId && d.PARENTS_ID == parentsId select d).ToList();
                if (data.Count == 0)
                    return true;

                if (isnew == true)
                {
                    var val = data.First().STATION_ID;
                    bool all_same = data.All(x => x.STATION_ID == stationId);
                    if (!all_same)
                        return false;
                }
                if (data.Count > 1)
                {
                    var val = data.First().STATION_ID;
                    bool all_same = data.All(x => x.STATION_ID == stationId);
                    if (!all_same)
                        return false;
                }
                return true;
            }
        }

        public static bool IsDuplicateChild(int parentsId, string amka)
        {
            int prosklisiId = Common.GetOpenProsklisiID();

            using (var db = new HermesDBEntities())
            {
                var children = (from d in db.CHILDREN where d.PARENTS_ID == parentsId && d.AMKA == amka select d).Count();
                if (children == 0)
                    return false;
                return true;
            }
        }

        public static bool IsDuplicateAitisi(int parentsId, int childId)
        {
            int prosklisiId = Common.GetOpenProsklisiID();

            using (var db = new HermesDBEntities())
            {
                var count = (from d in db.AITISIS
                             where d.PROSKLISI_ID == prosklisiId && d.PARENTS_ID == parentsId && d.CHILD_ID == childId
                             select d).Count();
                if (count == 0)
                    return false;
                return true;
            }
        }

        public static string ValidateAitisiMoria(AitisiCheckViewModel a)
        {
            string msg = "";

            if (!(a.RANKING > 0) || !(a.AGE_CATEGORY > 0))
                msg = "-> Πρέπει να επιλέξετε αξιολόγηση και κατηγορία ηλικίας.";

            if (a.RANKING == 3 && string.IsNullOrEmpty(a.APOKLEISMOS_AITIA))
                msg += "-> Για αποκλεισμό πρέπει να καταχωρήσετε και αιτία αποκλεισμού.";

            return msg;
        }

        public static bool ValidFileExtension(string extension)
        {
            string[] extensions = { ".PDF", ".JPG", ".JPEG" };

            List<string> allowed_extensions = new List<string>(extensions);

            if (allowed_extensions.Contains(extension.ToUpper()))
                return true;
            return false;
        }

        #endregion


        #region ΚΑΝΟΝΕΣ ΔΙΑΓΡΑΦΗΣ

        public static bool CanDeleteUserStation(int stationId)
        {
            using (var db = new HermesDBEntities())
            {
                int count1 = (from d in db.SYS_STATIONS where d.ΣΤΑΘΜΟΣ_ΚΩΔ == stationId select d).Count();

                if (count1 == 0)
                    return true;
                else
                    return false;
            }
        }

        public static bool CanDeleteStation(int stationId)
        {
            using (var db = new HermesDBEntities())
            {
                int count1 = (from d in db.AITISIS where d.STATION_ID == stationId select d).Count();
                int count2 = (from d in db.STATEMENTS where d.STATION_ID == stationId select d).Count();
                int count3 = (from d in db.UPLOADS where d.STATION_ID == stationId select d).Count();

                if (count1 == 0 && count2 == 0 && count3 == 0)
                    return true;
                else
                    return false;
            }
        }

        public static bool CanDeleteAitisi(int aitisiId) 
        {
            using (var db = new HermesDBEntities())
            {
                var data = (from d in db.UPLOADS where d.AITISI_ID == aitisiId select d).Count();
                if (data > 0)
                    return false;
                return true;
            }
        }

        public static bool CanDeleteProsklisi(int prosklisiId)
        {
            using (var db = new HermesDBEntities())
            {
                var data = (from d in db.AITISIS where d.PROSKLISI_ID == prosklisiId select d).Count();
                if (data > 0)
                    return false;
                return true;
            }
        }

        public static bool CanDeleteUserAccount(int userid)
        {
            int parentId = Common.GetParentIdFromUserId(userid);
            if (parentId > 0)
                return false;
            return true;
        }

        public static bool CanDeleteFamilyStatus(int fstatusId)
        {
            using (var db = new HermesDBEntities())
            {
                int count = (from d in db.PARENTS where d.FAMILY_STATUS == fstatusId select d).Count();

                if (count == 0)
                    return true;
                else
                    return false;
            }
        }

        public static bool CanDeleteNationality(int nationalityId)
        {
            using (var db = new HermesDBEntities())
            {
                int count = (from d in db.CHILDREN where d.NATIONALITY == nationalityId select d).Count();

                if (count == 0)
                    return true;
                else
                    return false;
            }
        }

        public static bool CanDeleteChildren(int childId)
        {
            using (var db = new HermesDBEntities())
            {
                var data = (from d in db.AITISIS where d.CHILD_ID == childId select d).Count();
                if (data > 0)
                    return false;
                return true;
            }
        }

        public static bool CanDeleteJobSector(int sectorId)
        {
            using (var db = new HermesDBEntities())
            {
                int count = (from d in db.STATEMENTS where d.FATHER_JOBSECTOR == sectorId || d.MOTHER_JOBSECTOR == sectorId select d).Count();

                if (count == 0)
                    return true;
                else
                    return false;
            }
        }

        public static bool CanDeleteUpload(int uploadId)
        {
            using (var db = new HermesDBEntities())
            {
                var data = (from d in db.UPLOADS_FILES where d.UPLOAD_ID == uploadId select d).Count();
                if (data > 0)
                    return false;
                return true;
            }
        }

        #endregion
    }
}