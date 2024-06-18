using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Hermes.DAL;
using Hermes.Filters;
using Hermes.Models;
using Hermes.BPM;
using Hermes.Notification;

namespace Hermes.Controllers.DataControllers
{
    [ErrorHandlerFilter]
    public class DocumentController : Controller
    {
        private readonly HermesDBEntities db;

        private USER_STATIONS loggedStation;
        private USER_ADMINS loggedAdmin;

        private const string UPLOAD_PATH = "~/Uploads/";

        public DocumentController(HermesDBEntities entities)
        {
            db = entities;
        }


        #region UPLOADED DOCUMENTS (STATION)

        public ActionResult sDownloadData(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_STATIONS");
            }
            else
            {
                loggedStation = GetLoginStation();
            }
            int prosklisiId = Common.GetActiveProsklisiID();
            if (prosklisiId == 0)
            {
                string Msg = "Δεν βρέθηκε ανοικτή Πρόσκληση.";
                return RedirectToAction("Index", "Station", new { notify = Msg });
            }
            int schoolYearId = (int)Common.GetActiveProsklisi().SCHOOL_YEAR;
            ViewData["prosklisiProtocol"] = Common.GetActiveProsklisi().PROTOCOL;
            ViewData["schoolYearText"] = Common.GetSchoolYearText(schoolYearId);

            if (notify != null)
            {
                this.ShowMessage(MessageType.Warning, notify);
            }
            if (!AitisisStationExist())
            {
                string msg = "Δεν βρέθηκαν αιτήσεις για το σταθμό αυτό. Η προβολή μεταφορτωμένων αρχείων είναι αδύνατη.";
                return RedirectToAction("Index", "Station", new { notify = msg });
            }

            PopulateSchoolYears();
            PopulateStationAitisis();

            return View();
        }

        #region MASTER GRID CRUD FUNCTIONS

        public ActionResult Upload_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<UploadsViewModel> data = GetStationUploadsFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Upload_Delete(int uploadId = 0)
        {
            string msg = "";
            UPLOADS entity = db.UPLOADS.Find(uploadId);
            if (entity != null)
            {
                if (Kerberos.CanDeleteUpload(entity.UPLOAD_ID))
                {
                    db.Entry(entity).State = EntityState.Deleted;
                    db.UPLOADS.Remove(entity);
                    db.SaveChanges();
                }
                else
                {
                    msg = "Για να γίνει η διαγραφή πρέπει πρώτα να διαγραφούν τα συνημμένα αρχεία μεταφόρτωσης.";
                }
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public List<UploadsViewModel> GetStationUploadsFromDB()
        {
            int stationId = (int)GetLoginStation().STATION_ID;
            int prosklisiId = Common.GetActiveProsklisiID();

            var data = (from d in db.UPLOADS
                        where d.PROSKLISI_ID == prosklisiId && d.STATION_ID == stationId
                        orderby d.UPLOAD_NAME, d.UPLOAD_DATE descending
                        select new UploadsViewModel
                        {
                            UPLOAD_ID = d.UPLOAD_ID,
                            AITISI_ID = d.AITISI_ID,
                            PARENT_ID = d.PARENT_ID,
                            STATION_ID = d.STATION_ID,
                            PROSKLISI_ID = d.PROSKLISI_ID,
                            UPLOAD_DATE = d.UPLOAD_DATE,
                            UPLOAD_NAME = d.UPLOAD_NAME,
                            UPLOAD_SUMMARY = d.UPLOAD_SUMMARY
                        }).ToList();

            return data;
        }

        #endregion


        #region CHILD GRID (UPLOADED FILEDETAILS)

        public ActionResult UploadFiles_Read([DataSourceRequest] DataSourceRequest request, int uploadId = 0)
        {
            List<UploadsFilesViewModel> data = GetUploadsFilesFromDB(uploadId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UploadFiles_Destroy([DataSourceRequest] DataSourceRequest request, UploadsFilesViewModel data)
        {
            // TODO: ALSO REMOVE UPLOADED FILES FROM SERVER
            if (data != null)
            {
                UPLOADS_FILES entity = db.UPLOADS_FILES.Find(data.ID);
                if (entity != null)
                {
                    // First delete the physical file and then the info record. Important!
                    DeleteUploadedFile(data.ID);

                    db.Entry(entity).State = EntityState.Deleted;
                    db.UPLOADS_FILES.Remove(entity);
                    db.SaveChanges();
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteUploadedFile(Guid file_guid)
        {
            string folder = "";
            string uploadPath = UPLOAD_PATH;
            string subfolder = "";
            string extension = "";

            var data = (from d in db.UPLOADS_FILES where d.ID == file_guid select d).FirstOrDefault();
            if (data != null)
            {
                folder = data.STATION_USER;
                subfolder = data.SCHOOLYEAR_TEXT;
                extension = data.EXTENSION;

                if (!String.IsNullOrEmpty(folder) && !String.IsNullOrEmpty(subfolder))
                    uploadPath += folder + "/" + subfolder + "/";

                string fileToDelete = file_guid + extension;
                var physicalPath = Path.Combine(Server.MapPath(uploadPath), fileToDelete);
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }
            return Content("");
        }

        public List<UploadsFilesViewModel> GetUploadsFilesFromDB(int uploadId = 0)
        {
            var data = (from d in db.UPLOADS_FILES
                        where d.UPLOAD_ID == uploadId
                        orderby d.STATION_USER, d.SCHOOLYEAR_TEXT, d.FILENAME
                        select new UploadsFilesViewModel
                        {
                            ID = d.ID,
                            UPLOAD_ID = d.UPLOAD_ID,
                            STATION_USER = d.STATION_USER,
                            SCHOOLYEAR_TEXT = d.SCHOOLYEAR_TEXT,
                            FILENAME = d.FILENAME,
                            EXTENSION = d.EXTENSION
                        }).ToList();

            return (data);
        }

        public FileResult Download(Guid file_id)
        {
            string p = "";
            string f = "";
            string the_path = UPLOAD_PATH;

            var fileinfo = (from d in db.UPLOADS_FILES where d.ID == file_id select d).FirstOrDefault();
            if (fileinfo != null)
            {
                the_path += fileinfo.STATION_USER + "/" + fileinfo.SCHOOLYEAR_TEXT + "/";
                p = fileinfo.ID.ToString() + fileinfo.EXTENSION;
                f = fileinfo.FILENAME;
            }

            return File(Path.Combine(Server.MapPath(the_path), p), System.Net.Mime.MediaTypeNames.Application.Octet, f);
        }

        #endregion


        #endregion

        #region UPLOADED DOCUMENTS (ADMIN)

        public ActionResult xDownloadData(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            int prosklisiId = Common.GetActiveProsklisiID();
            if (prosklisiId == 0)
            {
                string Msg = "Δεν βρέθηκε ενεργή Πρόσκληση.";
                return RedirectToAction("Index", "Admin", new { notify = Msg });
            }
            int schoolYearId = (int)Common.GetActiveProsklisi().SCHOOL_YEAR;
            ViewData["prosklisiProtocol"] = Common.GetActiveProsklisi().PROTOCOL;
            ViewData["schoolYearText"] = Common.GetSchoolYearText(schoolYearId);

            if (notify != null)
            {
                this.ShowMessage(MessageType.Warning, notify);
            }
            if (!AitisisGlobalExist())
            {
                string msg = "Δεν βρέθηκαν αιτήσεις για την Πρόσκληση αυτή. Η προβολή μεταφορτωμένων αρχείων είναι αδύνατη.";
                return RedirectToAction("Index", "Admin", new { notify = msg });
            }

            PopulateSchoolYears();
            PopulateGlobalAitisis();

            return View();
        }

        #region MASTER GRID CRUD FUNCTIONS

        public ActionResult xUpload_Read([DataSourceRequest] DataSourceRequest request, int stationId)
        {
            List<UploadsViewModel> data = GetAdminUploadsFromDB(stationId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult xUpload_Delete(int uploadId = 0)
        {
            string msg = "";
            UPLOADS entity = db.UPLOADS.Find(uploadId);
            if (entity != null)
            {
                if (Kerberos.CanDeleteUpload(entity.UPLOAD_ID))
                {
                    db.Entry(entity).State = EntityState.Deleted;
                    db.UPLOADS.Remove(entity);
                    db.SaveChanges();
                }
                else
                {
                    msg = "Για να γίνει η διαγραφή πρέπει πρώτα να διαγραφούν τα σχετικά αρχεία μεταφόρτωσης.";
                }
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public List<UploadsViewModel> GetAdminUploadsFromDB(int stationId)
        {
            int prosklisiId = Common.GetActiveProsklisiID();

            var data = (from d in db.UPLOADS
                        where d.PROSKLISI_ID == prosklisiId && d.STATION_ID == stationId
                        orderby d.UPLOAD_NAME, d.UPLOAD_DATE descending
                        select new UploadsViewModel
                        {
                            UPLOAD_ID = d.UPLOAD_ID,
                            AITISI_ID = d.AITISI_ID,
                            PARENT_ID = d.PARENT_ID,
                            STATION_ID = d.STATION_ID,
                            PROSKLISI_ID = d.PROSKLISI_ID,
                            UPLOAD_DATE = d.UPLOAD_DATE,
                            UPLOAD_NAME = d.UPLOAD_NAME,
                            UPLOAD_SUMMARY = d.UPLOAD_SUMMARY
                        }).ToList();
            return data;
        }

        #endregion


        #region CHILD GRID (UPLOADED FILEDETAILS)

        // All functions are the same as for stations

        #endregion

        #endregion


        #region GETTERS

        public JsonResult GetStations()
        {
            var data = (from d in db.SYS_STATIONS
                        orderby d.ΕΠΩΝΥΜΙΑ
                        select new StationViewModel
                        {
                            ΣΤΑΘΜΟΣ_ΚΩΔ = d.ΣΤΑΘΜΟΣ_ΚΩΔ,
                            ΕΠΩΝΥΜΙΑ = d.ΕΠΩΝΥΜΙΑ,
                            ΠΕΡΙΦΕΡΕΙΑΚΗ = d.ΠΕΡΙΦΕΡΕΙΑΚΗ
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public USER_ADMINS GetLoginAdmin()
        {
            loggedAdmin = db.USER_ADMINS.Where(u => u.USERNAME == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
            ViewBag.loggedAdmin = loggedAdmin;
            ViewBag.loggedUser = loggedAdmin.FULLNAME;
            return loggedAdmin;
        }

        public USER_STATIONS GetLoginStation()
        {
            loggedStation = db.USER_STATIONS.Where(u => u.USERNAME == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();

            int StationID = loggedStation.STATION_ID ?? 0;
            var _station = (from s in db.sqlUSER_STATION
                            where s.STATION_ID == StationID
                            select new { s.ΕΠΩΝΥΜΙΑ }).FirstOrDefault();

            ViewBag.loggedUser = _station.ΕΠΩΝΥΜΙΑ;
            return loggedStation;
        }


        #endregion


        #region POPULATORS

        public bool AitisisStationExist()
        {
            int stationId = (int)GetLoginStation().STATION_ID;
            int prosklisiId = Common.GetActiveProsklisiID();

            var aitiseis = (from d in db.AITISIS where d.PROSKLISI_ID == prosklisiId && d.STATION_ID == stationId select d).ToList();
            if (aitiseis.Count > 0)
                return true;
            return false;
        }

        public bool AitisisGlobalExist()
        {
            int prosklisiId = Common.GetActiveProsklisiID();

            var aitiseis = (from d in db.AITISIS where d.PROSKLISI_ID == prosklisiId select d).ToList();
            if (aitiseis.Count > 0)
                return true;
            return false;
        }

        public void PopulateStationAitisis()
        {
            int prosklisiId = Common.GetActiveProsklisiID();
            int stationId = (int)GetLoginStation().STATION_ID;

            var aitiseis = (from d in db.AITISIS where d.PROSKLISI_ID == prosklisiId && d.STATION_ID == stationId select d).ToList();

            ViewData["aitiseis"] = aitiseis;
            ViewData["defaultAitisi"] = aitiseis.First().AITISI_ID;
        }

        public void PopulateGlobalAitisis()
        {
            int prosklisiId = Common.GetActiveProsklisiID();
            var aitiseis = (from d in db.AITISIS where d.PROSKLISI_ID == prosklisiId select d).ToList();

            ViewData["aitiseis"] = aitiseis;
            ViewData["defaultAitisi"] = aitiseis.First().AITISI_ID;
        }

        public void PopulateSchoolYears()
        {
            var syears = (from s in db.SYS_SCHOOLYEARS
                          orderby s.SY_TEXT descending
                          select s).ToList();

            ViewData["school_years"] = syears;
            ViewData["defaultSchoolYear"] = syears.First().SY_ID;
        }

        #endregion


        #region MISCELLANEOUS

        public ActionResult ErrorData(string notify = null)
        {
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            return View();
        }

        public ActionResult Error(string notify = null)
        {
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            return View();
        }

        #endregion
    }
}
