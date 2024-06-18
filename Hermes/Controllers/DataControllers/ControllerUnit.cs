using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Hermes.DAL;
using Hermes.Models;
using Hermes.BPM;
using Hermes.Notification;
using Hermes.Services;

namespace Hermes.Controllers.DataControllers
{
    public class ControllerUnit : Controller
    {
        public USER_ADMINS loggedAdmin;
        public USER_STATIONS loggedStation;
        public USER_PARENTS loggedParent;
        public PARENTS loggedParentData;

        private readonly HermesDBEntities db;

        public ControllerUnit(HermesDBEntities entities)
        {
            db = entities;
        }

        #region POPULATORS

        public void PopulateChildren(int prosklisiId, int stationId)
        {
            var data = (from d in db.sqlCHILDREN_POPULATOR
                        where d.PROSKLISI_ID == prosklisiId && d.STATION_ID == stationId
                        orderby d.FULLNAME
                        select d).ToList();

            ViewData["children"] = data;
            ViewData["defaultChild"] = data.First().CHILD_ID;
        }

        public void PopulateChildren(int prosklisiId)
        {
            var data = (from d in db.sqlCHILDREN_POPULATOR
                        where d.PROSKLISI_ID == prosklisiId
                        orderby d.FULLNAME
                        select d).ToList();

            ViewData["children"] = data;
            ViewData["defaultChild"] = data.First().CHILD_ID;
        }

        public void PopulateAgeClasses()
        {
            var data = (from d in db.AGE_CLASSES select d).ToList();

            ViewData["age_classes"] = data;
        }

        public void PopulateIncomes()
        {
            var data = (from d in db.FAMILY_INCOME select d).ToList();

            ViewData["incomes"] = data;
        }

        public void PopulateParents()
        {
            var data = (from d in db.sqlPARENT_SELECTOR orderby d.PARENT_FULLNAME select d).ToList();

            ViewData["parents"] = data;
            ViewData["defaultParent"] = data.FirstOrDefault().PARENTS_ID;
        }

        public void PopulateNationalities()
        {
            var data = (from d in db.SYS_NATIONALITIES select d).ToList();

            ViewData["nationalities"] = data;
        }

        public void PopulateParentTypes()
        {
            var data = (from d in db.PARENT_TYPES select d).ToList();
            ViewData["parent_types"] = data;
        }

        public void PopulateJobSectors()
        {
            var data = (from s in db.JOB_SECTORS orderby s.JOBSECTOR_TEXT select s).ToList();

            ViewData["job_sectors"] = data;
        }

        public void PopulateGenders()
        {
            var data = (from s in db.SYS_GENDERS select s).ToList();

            ViewData["genders"] = data;
        }

        public void PopulateStatus()
        {
            var status = (from s in db.PROSKLISIS_STATUS select s).ToList();

            ViewData["Status"] = status;
        }

        public void PopulateSchoolYears()
        {
            var syears = (from s in db.SYS_SCHOOLYEARS
                          orderby s.SY_TEXT descending
                          select s).ToList();

            ViewData["SchoolYears"] = syears;
            ViewData["defaultSchoolYear"] = syears.First().SY_ID;
        }

        public void PopulateStations()
        {
            var stations = (from d in db.SYS_STATIONS where d.ΣΥΜΜΕΤΟΧΗ == true orderby d.ΕΠΩΝΥΜΙΑ select d).ToList();
            ViewData["stations"] = stations;
        }

        public void PopulatePeriferies()
        {
            var periferies = (from periferia in db.SYS_PERIFERIES select periferia).ToList();
            ViewData["Periferies"] = periferies;
        }

        public void PopulatePeriferiakes()
        {
            var data = (from d in db.SYS_PERIFERIAKES
                        orderby d.ΚΩΔΙΚΟΣ_ΠΕΡΙΦΕΡΕΙΑ
                        select d).ToList();

            ViewData["periferiakes"] = data;
        }

        public void PopulateAitisis()
        {
            int prosklisiId = Common.GetOpenProsklisiID();
            loggedParent = GetLoginParent();
            int parentId = Common.GetParentIdFromUserId(loggedParent.USER_ID);

            var aitiseis = (from d in db.AITISIS where d.PROSKLISI_ID == prosklisiId && d.PARENTS_ID == parentId select d).ToList();

            ViewData["aitiseis"] = aitiseis;
            ViewData["defaultAitisi"] = aitiseis.First().AITISI_ID;
        }

        public void PopulateParentChildren(int parentsId)
        {
            var data = (from d in db.sqlCHILD_SELECTOR where d.PARENTS_ID == parentsId orderby d.FULLNAME select d).ToList();

            ViewData["children"] = data;
            ViewData["defaultChild"] = data.First().CHILD_ID;
        }

        #endregion


        #region GETTERS, VALIDATORS

        public JsonResult GetProsklisis()
        {
            var data = (from d in db.PROSKLISIS
                        orderby d.DATE_START descending
                        select new ProsklisisViewModel
                        {
                            PROSKLISI_ID = d.PROSKLISI_ID,
                            PROTOCOL = d.PROTOCOL
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStations()
        {
            var data = (from d in db.SYS_STATIONS
                        where d.ΣΥΜΜΕΤΟΧΗ == true
                        orderby d.ΕΠΩΝΥΜΙΑ
                        select new StationViewModel
                        {
                            ΣΤΑΘΜΟΣ_ΚΩΔ = d.ΣΤΑΘΜΟΣ_ΚΩΔ,
                            ΕΠΩΝΥΜΙΑ = d.ΕΠΩΝΥΜΙΑ,
                            ΠΕΡΙΦΕΡΕΙΑΚΗ = d.ΠΕΡΙΦΕΡΕΙΑΚΗ
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAgeClasses()
        {
            var data = (from d in db.AGE_CLASSES select d).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRankings()
        {
            var data = (from d in db.RANKINGS
                        orderby d.RANKING_ID
                        select new RankingViewModel
                        {
                            RANKING_ID = d.RANKING_ID,
                            RANKING_TEXT = d.RANKING_TEXT
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFamilyIncome()
        {
            var data = (from d in db.FAMILY_INCOME
                        orderby d.INCOME_ID
                        select new FamilyIncomeViewModel
                        {
                            INCOME_ID = d.INCOME_ID,
                            INCOME_TEXT = d.INCOME_TEXT,
                            INCOME_MORIA = d.INCOME_MORIA
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetGenders()
        {
            var data = (from d in db.SYS_GENDERS
                        select new SYS_GENDERSViewModel
                        {
                            GENDER_ID = d.GENDER_ID,
                            GENDER = d.GENDER
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNationalities()
        {
            var data = (from d in db.SYS_NATIONALITIES
                        select new NationalityViewModel
                        {
                            NATIONALITY_ID = d.NATIONALITY_ID,
                            NATIONALITY_TEXT = d.NATIONALITY_TEXT
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetParents()
        {
            var data = (from d in db.sqlAPPLICANT_INFO
                        orderby d.PARENT_FULLNAME
                        select new ParentAccountInfoViewModel
                        {
                            PARENTS_ID = d.PARENTS_ID,
                            PARENT_FULLNAME = d.PARENT_FULLNAME
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetParentChildren()
        {
            int userId = GetLoginParent().USER_ID;
            int parentsId = Common.GetParentIdFromUserId(userId);

            var data = (from d in db.sqlCHILD_SELECTOR
                        where d.PARENTS_ID == parentsId
                        orderby d.FULLNAME
                        select new ChildSelectorViewModel
                        {
                            CHILD_ID = d.CHILD_ID,
                            FULLNAME = d.FULLNAME
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFamilyStatus()
        {
            var data = (from d in db.FAMILY_STATUS
                        orderby d.FSTATUS_TEXT
                        select new FamilyStatusViewModel
                        {
                            FSTATUS_ID = d.FSTATUS_ID,
                            FSTATUS_TEXT = d.FSTATUS_TEXT
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetParentTypes()
        {
            var data = (from d in db.PARENT_TYPES
                        select new ParentTypeViewModel
                        {
                            PARENT_TYPEID = d.PARENT_TYPEID,
                            PARENT_TYPETEXT = d.PARENT_TYPETEXT
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetJobSectors()
        {
            var data = (from d in db.JOB_SECTORS
                        orderby d.JOBSECTOR_TEXT
                        select new JobSectorViewModel
                        {
                            JOBSECTOR_ID = d.JOBSECTOR_ID,
                            JOBSECTOR_TEXT = d.JOBSECTOR_TEXT
                        }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNumbers()
        {
            var data = (from d in db.NUMBERS select d).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetYesNo()
        {
            var data = (from d in db.SYS_YESNO select d).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public USER_STATIONS GetLoginStation()
        {
            loggedStation = db.USER_STATIONS.Where(u => u.USERNAME == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();

            int stationId = loggedStation.STATION_ID ?? 0;
            var station = (from s in db.sqlUSER_STATION
                           where s.STATION_ID == stationId
                           select new { s.ΕΠΩΝΥΜΙΑ }).FirstOrDefault();

            ViewBag.loggedUser = station.ΕΠΩΝΥΜΙΑ;
            return loggedStation;
        }

        public USER_ADMINS GetLoginAdmin()
        {
            loggedAdmin = db.USER_ADMINS.Where(u => u.USERNAME == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
            ViewBag.loggedAdmin = loggedAdmin;
            ViewBag.loggedUser = loggedAdmin.FULLNAME;
            return loggedAdmin;
        }

        public USER_PARENTS GetLoginParent()
        {
            loggedParent = db.USER_PARENTS.Where(m => m.USER_AFM == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
            if (loggedParent == null)
                return null;

            ViewBag.loggedUser = loggedParent.USERNAME;

            loggedParentData = (from d in db.PARENTS where d.PARENT_USERID == loggedParent.USER_ID select d).FirstOrDefault();

            if (loggedParentData != null)
            {
                ViewBag.loggedParent = loggedParentData;

                if (loggedParent.PARENT_TYPE == 1)
                {
                    if (!string.IsNullOrEmpty(loggedParentData.FATHER_FIRSTNAME) && !string.IsNullOrEmpty(loggedParentData.FATHER_LASTNAME))
                    {
                        ViewBag.loggedUser = loggedParentData.FATHER_FIRSTNAME + " " + loggedParentData.FATHER_LASTNAME;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(loggedParentData.MOTHER_FIRSTNAME) && !string.IsNullOrEmpty(loggedParentData.MOTHER_LASTNAME))
                    {
                        ViewBag.loggedUser = loggedParentData.MOTHER_FIRSTNAME + " " + loggedParentData.MOTHER_LASTNAME;
                    }
                }
            }
            return loggedParent;
        }

        #endregion


        #region ERROR VIEWS

        public ActionResult Error(string notify = null)
        {
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            return View();
        }

        public ActionResult ErrorData(string notify = null)
        {
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            return View();
        }

        #endregion

    }
}