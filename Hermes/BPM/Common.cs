using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.Entity;
using Kendo.Mvc.Extensions;
using System.Globalization;
using Hermes.DAL;
using Hermes.Models;



namespace Hermes.BPM
{

    public static class Common
    {
        const decimal AGE1 = 1.50m;
        const decimal AGE2 = 2.50m;
        //const decimal AGE3 = 4.00m;

        const decimal INCOME1_MAX = 5600m;
        const decimal INCOME2_MIN = 5601m;
        const decimal INCOME2_MAX = 10000m;
        const decimal INCOME3_MIN = 10001m;
        const decimal INCOME3_MAX = 20000m;
        const decimal INCOME4_MIN = 20001m;
        const decimal INCOME4_MAX = 25000m;
        const decimal INCOME5_MIN = 25001m;
        const decimal INCOME5_MAX = 30000m;
        const decimal INCOME6_MIN = 30001m;
        const decimal INCOME6_MAX = 40000m;


        #region String Functions (equivalent to VB)

        public static string Right(string text, int numberCharacters)
        {
            return text.Substring(numberCharacters > text.Length ? 0 : text.Length - numberCharacters);
        }

        public static string Left(string text, int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException("length", length, "length must be > 0");
            else if (length == 0 || text.Length == 0)
                return "";
            else if (text.Length <= length)
                return text;
            else
                return text.Substring(0, length);
        }

        public static int Len(string text)
        {
            int _length;
            _length = text.Length;
            return _length;
        }

        public static byte Asc(string src)
        {
            return (System.Text.Encoding.GetEncoding("iso-8859-1").GetBytes(src + "")[0]);
        }

        public static char Chr(byte src)
        {
            return (System.Text.Encoding.GetEncoding("iso-8859-1").GetChars(new byte[] { src })[0]);
        }

        public static bool isNumber(string param)
        {
            Regex isNum = new Regex("[^0-9]");
            return !isNum.IsMatch(param);
        }

        #endregion


        #region Date Functions
        /// <summary>
        /// Μετατρέπει τον αριθμό του μήνα σε λεκτικό
        /// στη γενική πτώση.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static string monthToGRstring(int m)
        {
            string stGRmonth = "";

            switch (m)
            {
                case 1: stGRmonth = "Ιανουαρίου"; break;
                case 2: stGRmonth = "Φεβρουαρίου"; break;
                case 3: stGRmonth = "Μαρτίου"; break;
                case 4: stGRmonth = "Απριλίου"; break;
                case 5: stGRmonth = "Μαϊου"; break;
                case 6: stGRmonth = "Ιουνίου"; break;
                case 7: stGRmonth = "Ιουλίου"; break;
                case 8: stGRmonth = "Αυγούστου"; break;
                case 9: stGRmonth = "Σεπτεμβρίου"; break;
                case 10: stGRmonth = "Οκτωβρίου"; break;
                case 11: stGRmonth = "Νοεμβρίου"; break;
                case 12: stGRmonth = "Δεκεμβρίου"; break;
                default: break;
            }
            return stGRmonth;
        }

        /// <summary>
        /// Ελέγχει αν η αρχική ημερομηνία είναι μικρότερη
        /// ή ίση με την τελική ημερομηνία.
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public static bool ValidStartEndDates(DateTime dateStart, DateTime dateEnd)
        {
            bool result;

            if (dateStart > dateEnd)
                result = false;
            else
                result = true;
            return result;
        }

        /// <summary>
        /// Ελέγχει αν δύο ημερομηνίες ανήκουν στο ίδιο έτος.
        /// </summary>
        /// <param name="date1"></param>
        /// <param name="date2"></param>
        /// <returns></returns>
        public static bool DatesInSameYear(DateTime date1, DateTime date2)
        {
            bool result;

            if (date1.Year != date2.Year)
                result = false;
            else
                result = true;
            return result;
        }

        /// <summary>
        /// Ελέγχει αν δύο ημερομηνίες είναι μέσα στο ίδιο Σχ. Έτος
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <param name="schoolYearID"></param>
        /// <returns></returns>
        public static bool DatesInSchoolYear(DateTime dateStart, DateTime dateEnd, int schoolYearID)
        {
            bool result = true;

            using (var db = new HermesDBEntities())
            {
                var schoolYear = (from s in db.SYS_SCHOOLYEARS
                                  where s.SY_ID == schoolYearID
                                  select new { s.SY_DATESTART, s.SY_DATEEND }).FirstOrDefault();

                if (dateStart < schoolYear.SY_DATESTART || dateEnd > schoolYear.SY_DATEEND)
                    result = false;

                return result;
            }
        }

        /// <summary>
        /// Ελέγχει αν το σχολικό έτος έχει τη μορφή ΝΝΝΝ-ΝΝΝΝ
        /// και αν τα έτη είναι συμβατά με τις ημερομηνίες
        /// έναρξης και λήξης.
        /// </summary>
        /// <param name="syear"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static bool VerifySchoolYear(string syear, DateTime d1, DateTime d2)
        {

            if (syear.IndexOf('-') == -1)
            {
                //ShowAdminMessage("Το σχολικό έτος πρέπει να είναι στη μορφή έτος1-έτος2.");
                return false;
            }

            string[] split = syear.Split(new Char[] { '-' });
            string sy1 = Convert.ToString(split[0]);
            string sy2 = Convert.ToString(split[1]);

            if (!isNumber(sy1) || !isNumber(sy2))
            {
                //ShowAdminMessage("Τα έτη δεν είναι αριθμοί.");
                return false;
            }
            else
            {
                int y1 = Convert.ToInt32(sy1);
                int y2 = Convert.ToInt32(sy2);

                if (y2 - y1 > 1 || y2 - y1 <= 0)
                {
                    //UserFunctions.ShowAdminMessage("Τα έτη δεν είναι σωστά.");
                    return false;
                }
                if (d1.Year != y1 || d2.Year != y2)
                {
                    //UserFunctions.ShowAdminMessage("Οι ημερομηνίες δεν συμφωνούν με τα έτη.");
                    return false;
                }
            }
            // at this point everything is ok
            return true;
        }

        /// <summary>
        /// Ελέγχει αν το χολικό έτος μορφής ΝΝΝΝ-ΝΝΝΝ υπάρχει ήδη.
        /// </summary>
        /// <param name="syear"></param>
        /// <returns></returns>
        public static bool SchoolYearExists(int syear)
        {
            using (var db = new HermesDBEntities())
            {
                var syear_recs = (from s in db.SYS_SCHOOLYEARS
                                  where s.SY_ID == syear
                                  select s).Count();

                if (syear_recs != 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Υπολογίζει τα έτη (στρογγυλοποιημένα) μεταξύ δύο ημερομηνιών
        /// </summary>
        /// <param name="sdate">αρχική ημερομηνία</param>
        /// <param name="edate">τελική ημερομηνία</param>
        /// <returns></returns>
        public static int YearsDiff(DateTime sdate, DateTime edate)
        {
            TimeSpan ts = edate - sdate;
            int days = ts.Days;

            double _years = days / 365;

            int years = Convert.ToInt32(Math.Ceiling(_years));

            return years;
        }

        public static int DaysDiff(DateTime sdate, DateTime edate)
        {
            TimeSpan ts = edate - sdate;
            int days = ts.Days;

            return days;
        }

        /// <summary>
        /// Υπολογίζει τις ημέρες λογιστικού έτους μεταξύ δύο ημερομηνιών,
        /// προσομειώνοντας τη συνάρτηση Days360 του Excel.
        /// </summary>
        /// <param name="initial_date"></param>
        /// <param name="final_date"></param>
        /// <returns name="meres"></returns>
        public static int Days360(DateTime initial_date, DateTime final_date)
        {
            DateTime date1 = initial_date;
            DateTime date2 = final_date;

            var y1 = date1.Year;
            var y2 = date2.Year;
            var m1 = date1.Month;
            var m2 = date2.Month;
            var d1 = date1.Day;
            var d2 = date2.Day;

            DateTime tempDate = date1.AddDays(1);
            if (tempDate.Day == 1 && date1.Month == 2)
            {
                d1 = 30;
            }
            if (d2 == 31 && d1 == 30)
            {
                d2 = 30;
            }

            double meres = (y2 - y1) * 360 + (m2 - m1) * 30 + (d2 - d1);
            meres = (meres / 30) * 25;
            meres = Math.Ceiling(meres);

            return Convert.ToInt32(meres);
        }

        #endregion


        #region Custom Hermes Functions

        public static float Max(params float[] values)
        {
            return Enumerable.Max(values);
        }

        public static float Min(params float[] values)
        {
            return Enumerable.Min(values);
        }

        public static string GetUserStationFromStationId(int stationId)
        {
            string username = "";

            using (var db = new HermesDBEntities())
            {
                var data = (from d in db.USER_STATIONS where d.STATION_ID == stationId select d).FirstOrDefault();
                if (data != null)
                {
                    username = data.USERNAME;
                }
                return (username);
            }
        }

        public static decimal ComputeChildAge(int childId)
        {
            decimal age = 0.0m;
            string sref_date = "01/09/";

            using (var db = new HermesDBEntities())
            {
                var data = (from d in db.CHILDREN where d.CHILD_ID == childId select d).FirstOrDefault();
                var prosklisi = GetActiveProsklisi();

                if (data == null || prosklisi == null)
                    return age;

                DateTime? birthdate = data.BIRTHDATE;
                DateTime refdate = (DateTime)prosklisi.DATE_START;

                string syear = refdate.Year.ToString();
                string RefDate = sref_date + syear;

                refdate = DateTime.ParseExact(RefDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (birthdate == null) return age;  // if no valid birthdate abort calculation

                DateTime today = DateTime.Today;

                if (refdate != null) today = (DateTime)refdate;
                // get the last birthday
                DateTime birthDate = (DateTime)birthdate;

                int years = today.Year - birthDate.Year;
                DateTime last = birthDate.AddYears(years);
                if (last > today)
                {
                    last = last.AddYears(-1);
                    years--;
                }
                // get the next birthday
                DateTime next = last.AddYears(1);
                // calculate the number of days between them
                double yearDays = (next - last).Days;
                // calcluate the number of days since last birthday
                double days = (today - last).Days;
                // calculate exaxt age
                double exactAge = (double)years + (days / yearDays);

                age = Math.Round((decimal)exactAge, 3);

                return (age);
            }
        }

        public static int ComputeAgeCategory(int childId)
        {
            int category = 0;
            int single_day = 3;     // one day = 1/360 = 0.003, and as 3-digit precision = 3

            decimal age = ComputeChildAge(childId);

            decimal _age = Math.Truncate(age * 1000);
            decimal _AGE1 = Math.Truncate(AGE1 * 1000);
            decimal _AGE2 = Math.Truncate(AGE2 * 1000);

            if (_age <= _AGE1)
                category = 1;
            else if (_age >= _AGE1 + single_day && _age <= _AGE2)
                category = 2;
            else if (_age >= _AGE2 + single_day)
                category = 3;

            return (category);
        }

        public static int GetOpenProsklisiID()
        {
            using (var db = new HermesDBEntities())
            {
                var data = (from p in db.PROSKLISIS
                            where p.STATUS == 1
                            select new { p.PROSKLISI_ID }).FirstOrDefault();

                if (data == null)
                    return 0;
                else
                    return data.PROSKLISI_ID;
            }
        }

        public static int GetActiveProsklisiID()
        {
            using (var db = new HermesDBEntities())
            {
                var data = (from p in db.PROSKLISIS
                            where p.ACTIVE == true
                            select new { p.PROSKLISI_ID }).FirstOrDefault();

                if (data == null)
                    return 0;
                else
                    return data.PROSKLISI_ID;
            }
        }

        public static int GetOpenProsklisiID(bool active)
        {
            using (var db = new HermesDBEntities())
            {
                var data = (from p in db.PROSKLISIS
                            where p.ACTIVE == true
                            select new { p.PROSKLISI_ID }).FirstOrDefault();

                if (data == null)
                    return 0;
                else
                    return data.PROSKLISI_ID;
            }
        }

        public static ProsklisisViewModel GetOpenProsklisi()
        {
            using (var db = new HermesDBEntities())
            {
                var data = (from d in db.PROSKLISIS
                            where d.STATUS == 1
                            select new ProsklisisViewModel
                            {
                                PROSKLISI_ID = d.PROSKLISI_ID,
                                PROTOCOL = d.PROTOCOL,
                                SCHOOL_YEAR = d.SCHOOL_YEAR,
                                DIOIKITIS = d.DIOIKITIS,
                                DATE_START = d.DATE_START,
                                DATE_END = d.DATE_END,
                                HOUR_START = d.HOUR_START,
                                HOUR_END = d.HOUR_END
                            }).FirstOrDefault();
                return (data);
            }
        }

        public static ProsklisisViewModel GetActiveProsklisi()
        {
            using (var db = new HermesDBEntities())
            {
                var data = (from d in db.PROSKLISIS
                            where d.ACTIVE == true
                            select new ProsklisisViewModel
                            {
                                PROSKLISI_ID = d.PROSKLISI_ID,
                                PROTOCOL = d.PROTOCOL,
                                SCHOOL_YEAR = d.SCHOOL_YEAR,
                                DIOIKITIS = d.DIOIKITIS,
                                DATE_START = d.DATE_START,
                                DATE_END = d.DATE_END,
                                HOUR_START = d.HOUR_START,
                                HOUR_END = d.HOUR_END
                            }).FirstOrDefault();
                return (data);
            }
        }

        public static bool GetProsklisiUserView()
        {
            using (var db = new HermesDBEntities())
            {
                var data = (from p in db.PROSKLISIS
                            where p.USERVIEW == true
                            select new { p.USERVIEW }).FirstOrDefault();

                if (data == null)
                    return false;
                else
                    return (bool)data.USERVIEW;
            }
        }

        public static string GetSchoolYearText(int syearId)
        {
            using (var db = new HermesDBEntities())
            {
                var data = (from d in db.SYS_SCHOOLYEARS
                            where d.SY_ID == syearId
                            select d).FirstOrDefault();

                string syearText = data.SY_TEXT;
                return (syearText);
            }
        }

        public static int GetParentsId(string afm)
        {
            using (var db = new HermesDBEntities())
            {
                var data = (from d in db.PARENTS where d.FATHER_AFM == afm || d.MOTHER_AFM == afm select d).FirstOrDefault();
                if (data != null)
                    return data.PARENTS_ID;
                else
                    return 0;
            }
        }

        public static int GetStationIdFromAitisi(int aitisiId)
        {
            using (var db = new HermesDBEntities())
            {
                var data = (from d in db.AITISIS where d.AITISI_ID == aitisiId select d).FirstOrDefault();
                if (data != null)
                    return (int)data.STATION_ID;
                else
                    return 0;
            }
        }

        public static int GetStationIdFromParent(int prosklisiId, int parentsId)
        {
            using (var db = new HermesDBEntities())
            {
                var data = (from d in db.AITISIS where d.PROSKLISI_ID == prosklisiId && d.PARENTS_ID == parentsId select d).FirstOrDefault();
                if (data != null)
                    return (int)data.STATION_ID;
                return 0;
            }
        }

        public static int GetStatementIdFromParent(int prosklisiId, int parentsId)
        {
            using (var db = new HermesDBEntities())
            {
                var data = (from d in db.STATEMENTS where d.PROSKLISI_ID == prosklisiId && d.PARENTS_ID == parentsId select d).FirstOrDefault();
                if (data != null)
                    return (int)data.STATEMENT_ID;
                return 0;
            }
        }

        public static string GetUserStationFromAitisi(int aitisiId)
        {
            int stationId = GetStationIdFromAitisi(aitisiId);

            using (var db = new HermesDBEntities())
            {
                var data = (from d in db.USER_STATIONS where d.STATION_ID == stationId select d).FirstOrDefault();
                if (data != null)
                {
                    return data.USERNAME;
                }
                else
                {
                    return "vns.demo";
                }
            }
        }

        public static string GetParentNameFromUser(USER_PARENTS loggedParent)
        {
            string fullname = "X";

            using (var db = new HermesDBEntities())
            {
                if (loggedParent.PARENT_TYPE == 1)
                {
                    var parent = (from d in db.PARENTS where d.PARENT_USERID == loggedParent.USER_ID select d).FirstOrDefault();
                    if (parent != null)
                        fullname = parent.FATHER_LASTNAME + " " + parent.FATHER_FIRSTNAME;
                }
                else
                {
                    var parent = (from d in db.PARENTS where d.PARENT_USERID == loggedParent.USER_ID select d).FirstOrDefault();
                    if (parent != null)
                        fullname = parent.MOTHER_LASTNAME + " " + parent.MOTHER_FIRSTNAME;
                }
                return (fullname);
            }
        }

        public static int GetParentIdFromUserId(int userId)
        {
            using (var db = new HermesDBEntities())
            {
                var parents = (from d in db.PARENTS where d.PARENT_USERID == userId select d).FirstOrDefault();
                if (parents != null)
                {
                    return parents.PARENTS_ID;
                }
                return 0;
            }
        }

        public static string GetOpenProsklisiProtocol(int aitisiId)
        {
            string prosklisi_protocol = "";
            int prosklisiId = GetOpenProsklisiID();

            using (var db = new HermesDBEntities())
            {
                var prokname = (from p in db.PROSKLISIS
                                where p.PROSKLISI_ID == prosklisiId
                                select new { p.PROTOCOL }).FirstOrDefault();

                prosklisi_protocol = prokname.PROTOCOL;
                return prosklisi_protocol;
            }
        }

        public static PARENTS GetParentDataFromStatement(int statementId)
        {
            using (var db = new HermesDBEntities())
            {
                var data1 = (from d in db.STATEMENTS where d.STATEMENT_ID == statementId select d).FirstOrDefault();
                if (data1 == null)
                    return null;

                var data2 = (from d in db.PARENTS where d.PARENTS_ID == data1.PARENTS_ID select d).FirstOrDefault();
                return data2;
            }
        }

        public static bool ParentAitisiExists(int parentsId, int prosklisiId)
        {
            using (var db = new HermesDBEntities())
            {
                var count = (from d in db.AITISIS where d.PARENTS_ID == parentsId && d.PROSKLISI_ID == prosklisiId select d).Count();
                if (count > 0)
                    return true;
                return false;
            }
        }

        public static bool ParentStatementExists(int parentsId, int prosklisiId)
        {
            using (var db = new HermesDBEntities())
            {
                var count = (from d in db.STATEMENTS where d.PARENTS_ID == parentsId && d.PROSKLISI_ID == prosklisiId select d).Count();
                if (count > 0)
                    return true;
                return false;
            }
        }

        public static int ComputeIncomeCategory(decimal? income)
        {
            int category = 0;

            if (income == null) return category;

            decimal income_rnd = Math.Round((decimal)income, 0, MidpointRounding.AwayFromZero);

            if (income_rnd <= INCOME1_MAX)
                category = 1;
            else if (income_rnd >= INCOME2_MIN && income_rnd <= INCOME2_MAX)
                category = 2;
            else if (income_rnd >= INCOME3_MIN && income_rnd <= INCOME3_MAX)
                category = 3;
            else if (income_rnd >= INCOME4_MIN && income_rnd <= INCOME4_MAX)
                category = 4;
            else if (income_rnd >= INCOME5_MIN && income_rnd <= INCOME5_MAX)
                category = 5;
            else if (income_rnd >= INCOME6_MIN && income_rnd <= INCOME6_MAX)
                category = 6;
            else if (income_rnd > INCOME6_MAX)
                category = 7;

            return category;
        }

        public static string VerifyIncomeCategory(int? IncomeCategory)
        {
            if (IncomeCategory > 0)
                return "";

            return "Αδυναμία υπολογισμού κατηγορίας εισοδήματος. Δοκιμάστε πλησιέστερο ακέραιο για το εισόδημα.";
        }

        public static void UpdateAitisiChildAge(int childId)
        {
            int prosklisiId = GetActiveProsklisiID();

            using (var db = new HermesDBEntities())
            {
                var data = (from d in db.AITISIS where d.CHILD_ID == childId && d.PROSKLISI_ID == prosklisiId select d).FirstOrDefault();
                if (data == null)
                    return;

                AITISIS entity = db.AITISIS.Find(data.AITISI_ID);

                entity.AGE = ComputeChildAge((int)data.CHILD_ID);
                entity.AGE_CATEGORY = ComputeAgeCategory((int)data.CHILD_ID);

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        #endregion


        #region Protocol Generator

        public static string Get6Digits()
        {
            //var bytes = new byte[4];
            //var rng = RandomNumberGenerator.Create();
            //rng.GetBytes(bytes);
            //uint random = BitConverter.ToUInt32(bytes, 0) % 100000000;
            //return String.Format("{0:D8}", random);
            int newId = 0;

            using (var db = new HermesDBEntities())
            {
                var data = (from d in db.AITISIS orderby d.AITISI_ID descending select d).FirstOrDefault();
                if (data == null)
                    newId += 1;
                else
                    newId = data.AITISI_ID + 1;

                return string.Format("{0:000000}", newId);
            }
        }

        /*
         * ------------------------------------------------------------------
         * Protocol unique number generator.
         * It is based on RundomNumberGenerator (Microsoft cryptography)
         * and Get8Digits in Common.cs of Hermes
         * ------------------------------------------------------------------
         */

        public static string GenerateProtocol()
        {
            DateTime date1 = DateTime.Now;
            DateTime dateOnly = date1.Date;

            string stDate = string.Format("{0:dd.MM.yyyy}", dateOnly);               //Convert.ToString(dateOnly);

            string protocol = Get6Digits() + "/" + stDate;
            return protocol;
        }

        public static string GenerateProtocol(DateTime date1)
        {
            DateTime dateOnly = date1.Date;

            string stDate = string.Format("{0:dd.MM.yyyy}", dateOnly);               //Convert.ToString(dateOnly);

            string protocol = Get6Digits() + "/" + stDate;
            return protocol;
        }

        public static string GeneratePassword()
        {
            Random rnd = new Random();
            int random = rnd.Next(1, 1000);
            return string.Format("{0:000}", random);
        }

        #endregion


        #region Upload Functions

        public static bool VerifyUploadIntegrity(int prosklisiId, USER_PARENTS loggedParent)
        {
            int parentsId = GetParentIdFromUserId(loggedParent.USER_ID);

            using (var db = new HermesDBEntities())
            {
                var data = (from d in db.UPLOADS where d.PROSKLISI_ID == prosklisiId && d.PARENT_ID == parentsId select d).FirstOrDefault();
                if (data == null)
                    return true;

                if (!UploadFilesExist(data.UPLOAD_ID))
                    return true;

                string username = GetUserStationFromStationId((int)data.STATION_ID);

                var files = (from d in db.UPLOADS_FILES where d.UPLOAD_ID == data.UPLOAD_ID && d.STATION_USER == username select d).Count();
                if (files == 0)
                    return false;
                else
                    return true;
            }
        }

        public static bool UploadFilesExist(int uploadId)
        {
            using (var db = new HermesDBEntities())
            {
                int countFiles = (from d in db.UPLOADS_FILES where d.UPLOAD_ID == uploadId select d).Count();
                if (countFiles > 0)
                    return true;
                return false;
            }
        }

        public static string GetParentUsername(int userId)
        {
            string username = "";

            using (var db = new HermesDBEntities())
            {
                var data2 = (from d in db.USER_PARENTS where d.USER_ID == userId select d).FirstOrDefault();
                if (data2 != null) username = data2.USERNAME;

                return (username);
            }
        }

        public static Guid GetFileGuidFromName(string filename, int uploadId)
        {
            Guid file_id = new Guid();

            using (var db = new HermesDBEntities())
            {
                var fileData = (from d in db.UPLOADS_FILES where d.FILENAME == filename && d.UPLOAD_ID == uploadId select d).FirstOrDefault();
                if (fileData != null) file_id = fileData.ID;

                return (file_id);
            }
        }

        public static Tuple<int, int, int> GetUploadInfo(int uploadId)
        {
            int station_id = 0;
            int prosklisi_id = 0;
            int aitisi_id = 0;

            using (var db = new HermesDBEntities())
            {
                var upload = (from d in db.UPLOADS where d.UPLOAD_ID == uploadId select d).FirstOrDefault();
                if (upload != null)
                {
                    station_id = (int)upload.STATION_ID;
                    prosklisi_id = (int)upload.PROSKLISI_ID;
                    aitisi_id = (int)upload.AITISI_ID;
                }

                var data = Tuple.Create(station_id, prosklisi_id, aitisi_id);
                return (data);
            }
        }

        #endregion

    }   // class Common

}   // namespace