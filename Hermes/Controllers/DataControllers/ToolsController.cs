using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Hermes.DAL;
using Hermes.Models;
using Hermes.BPM;
using Hermes.Filters;
using Hermes.Notification;
using Hermes.Services;

namespace Hermes.Controllers.DataControllers
{
    [ErrorHandlerFilter]
    public class ToolsController : ControllerUnit
    {
        private readonly HermesDBEntities db;

        private readonly ISchoolYearService schoolYearService;
        private readonly IProsklisiService prosklisiService;
        private readonly IStationDataService stationDataService;
        private readonly IFamilyStatusService familyStatusService;
        private readonly INationalityService nationalityService;
        private readonly IJobSectorService jobSectorService;

        public ToolsController(HermesDBEntities entities, 
            ISchoolYearService schoolYearService, IProsklisiService prosklisiService,
            IStationDataService  stationDataService, IFamilyStatusService familyStatusService,
            INationalityService nationalityService, IJobSectorService jobSectorService) : base(entities)
        {
            db = entities;

            this.schoolYearService = schoolYearService;
            this.prosklisiService = prosklisiService;
            this.stationDataService = stationDataService;
            this.familyStatusService = familyStatusService;
            this.nationalityService = nationalityService;
            this.jobSectorService = jobSectorService;
        }


        #region ΣΧΟΛΙΚΑ ΕΤΗ

        public ActionResult SchoolYearsList()
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

            return View();
        }

        public ActionResult SchoolYear_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<SchoolYearsViewModel> data = schoolYearService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SchoolYear_Create([DataSourceRequest] DataSourceRequest request, SchoolYearsViewModel data)
        {
            var newData = new SchoolYearsViewModel();

            var existingSchoolYears = db.SYS_SCHOOLYEARS.Where(s => s.SY_TEXT == data.SY_TEXT).Count();
            if (existingSchoolYears > 0)
                ModelState.AddModelError("", "Το σχολικό έτος είναι ήδη καταχωρημένο. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                schoolYearService.Create(data);
                newData = schoolYearService.Refresh(data.SY_ID);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SchoolYear_Update([DataSourceRequest] DataSourceRequest request, SchoolYearsViewModel data)
        {
            var newData = new SchoolYearsViewModel();

            if (data != null & ModelState.IsValid)
            {
                schoolYearService.Update(data);
                newData = schoolYearService.Refresh(data.SY_ID);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SchoolYear_Destroy([DataSourceRequest] DataSourceRequest request, SchoolYearsViewModel data)
        {
            if (data != null)
            {
                schoolYearService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΠΡΟΣΚΛΗΣΕΙΣ

        public ActionResult ProsklisisList()
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

            PopulateSchoolYears();
            PopulateStatus();

            return View();
        }

        [HttpPost]
        public ActionResult Prosklisis_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<ProsklisisViewModel> data = prosklisiService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Prosklisis_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ProsklisisViewModel> data)
        {
            var results = new List<ProsklisisViewModel>();

            if (data != null && ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    prosklisiService.Create(item);
                    results.Add(item);
                }
            }
            return Json(results.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Prosklisis_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ProsklisisViewModel> data)
        {
            foreach (var item in data)
            {
                if (item != null && ModelState.IsValid)
                {
                    prosklisiService.Update(item);
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Prosklisis_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<ProsklisisViewModel> data)
        {
            if (data.Any())
            {
                foreach (var item in data)
                {
                    if (Kerberos.CanDeleteProsklisi(item.PROSKLISI_ID))
                    {
                        prosklisiService.Destroy(item);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Δεν μπορεί να διαγραφεί η πρόσκληση διότι υπάρχουν συσχετισμένες αιτήσεις");
                    }
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΒΡΕΦΟΝΗΠΙΑΚΟΙ ΣΤΑΘΜΟΙ

        public ActionResult StationDataList()
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
            int schoolYearId = (int)Common.GetActiveProsklisi().SCHOOL_YEAR;

            ViewData["prosklisiProtocol"] = Common.GetActiveProsklisi().PROTOCOL;
            ViewData["schoolYearText"] = Common.GetSchoolYearText(schoolYearId);

            PopulatePeriferiakes();
            PopulatePeriferies();

            return View();
        }

        [HttpPost]
        public ActionResult Station_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<StationsGridViewModel> data = stationDataService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Station_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<StationsGridViewModel> data)
        {
            var results = new List<StationsGridViewModel>();

            if (data != null && ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    stationDataService.Create(item);
                    results.Add(item);
                }
            }
            return Json(results.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Station_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<StationsGridViewModel> data)
        {
            foreach (var item in data)
            {
                if (item != null && ModelState.IsValid)
                {
                    stationDataService.Update(item);
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Station_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<StationsGridViewModel> data)
        {
            if (data.Any())
            {
                foreach (var item in data)
                {
                    if (item != null)
                    {
                        if (Kerberos.CanDeleteStation(item.ΣΤΑΘΜΟΣ_ΚΩΔ))
                        {
                            stationDataService.Destroy(item);
                        }
                        else
                        {
                            ModelState.AddModelError("", "Δεν μπορει να διαγραφεί ο βρεφονηπιακός σταθμός διότι είναι σε χρήση.");
                        }
                    }
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΟΙΚΟΓΕΝΕΙΑΚΗ ΚΑΤΑΣΤΑΣΗ

        public ActionResult FamilyStatusList()
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
            return View();
        }

        public ActionResult FamilyStatus_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<FamilyStatusViewModel> data = familyStatusService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FamilyStatus_Create([DataSourceRequest] DataSourceRequest request, FamilyStatusViewModel data)
        {
            var newData = new FamilyStatusViewModel();

            var existingdata = db.FAMILY_STATUS.Where(s => s.FSTATUS_TEXT == data.FSTATUS_TEXT).Count();
            if (existingdata > 0) 
                ModelState.AddModelError("", "Αυτή η οικογ. κατάσταση υπάρχει ήδη. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                familyStatusService.Create(data);

                newData = familyStatusService.Refresh(data.FSTATUS_ID);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FamilyStatus_Update([DataSourceRequest] DataSourceRequest request, FamilyStatusViewModel data)
        {
            var newData = new FamilyStatusViewModel();

            if (data != null && ModelState.IsValid)
            {
                familyStatusService.Update(data);

                newData = familyStatusService.Refresh(data.FSTATUS_ID);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult FamilyStatus_Destroy([DataSourceRequest] DataSourceRequest request, FamilyStatusViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteFamilyStatus(data.FSTATUS_ID))
                {
                    familyStatusService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Η διαγραφή δεν είναι δυνατή γιατί η τιμή αυτή χρησιμοποιείται ήδη.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΕΘΝΙΚΟΤΗΤΕΣ

        public ActionResult NationalitiesList()
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
            return View();
        }

        public ActionResult Nationality_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<NationalityViewModel> data = nationalityService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Nationality_Create([DataSourceRequest] DataSourceRequest request, NationalityViewModel data)
        {
            var newData = new NationalityViewModel();

            var existingdata = db.SYS_NATIONALITIES.Where(s => s.NATIONALITY_TEXT == data.NATIONALITY_TEXT).Count();
            if (existingdata > 0) 
                ModelState.AddModelError("", "Αυτή η εθνικότητα υπάρχει ήδη. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                nationalityService.Create(data);
                newData = nationalityService.Refresh(data.NATIONALITY_ID);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Nationality_Update([DataSourceRequest] DataSourceRequest request, NationalityViewModel data)
        {
            var newData = new NationalityViewModel();

            if (data != null && ModelState.IsValid)
            {
                nationalityService.Update(data);
                newData = nationalityService.Refresh(data.NATIONALITY_ID);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Nationality_Destroy([DataSourceRequest] DataSourceRequest request, NationalityViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteNationality(data.NATIONALITY_ID))
                {
                    nationalityService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Η διαγραφή δεν είναι δυνατή γιατί η τιμή αυτή χρησιμοποιείται ήδη.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΤΟΜΕΙΣ ΕΠΑΓΓΕΛΜΑΤΟΣ

        public ActionResult JobSectorsList()
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
            return View();
        }

        public ActionResult JobSector_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<JobSectorViewModel> data = jobSectorService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JobSector_Create([DataSourceRequest] DataSourceRequest request, JobSectorViewModel data)
        {
            var newData = new JobSectorViewModel();

            var existingdata = db.JOB_SECTORS.Where(s => s.JOBSECTOR_TEXT == data.JOBSECTOR_TEXT).Count();
            if (existingdata > 0) 
                ModelState.AddModelError("", "Αυτός ο τομέας επαγγέλματος υπάρχει ήδη. Η καταχώρηση ακυρώθηκε.");

            if (data != null && ModelState.IsValid)
            {
                jobSectorService.Create(data);
                newData = jobSectorService.Refresh(data.JOBSECTOR_ID);
            }
            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JobSector_Update([DataSourceRequest] DataSourceRequest request, JobSectorViewModel data)
        {
            var newData = new JobSectorViewModel();

            if (data != null && ModelState.IsValid)
            {
                jobSectorService.Update(data);
                newData = jobSectorService.Refresh(data.JOBSECTOR_ID);
            }

            return Json(new[] { newData }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult JobSector_Destroy([DataSourceRequest] DataSourceRequest request, JobSectorViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteJobSector(data.JOBSECTOR_ID))
                {
                    jobSectorService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Η διαγραφή δεν είναι δυνατή γιατί η τιμή αυτή χρησιμοποιείται ήδη.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΠΕΡΙΦΕΡΕΙΕΣ-ΔΗΜΟΙ

        public ActionResult PeriferiesDimoi()
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
            return View();
        }

        public ActionResult Periferies([DataSourceRequest] DataSourceRequest request)
        {
            var periferies = db.SYS_PERIFERIES.Select(p => new PeriferiaViewModel()
            {
                PERIFERIA_ID = p.PERIFERIA_ID,
                PERIFERIA_NAME = p.PERIFERIA_NAME
            });
            return Json(periferies.ToDataSourceResult(request));
        }

        public ActionResult Dimoi([DataSourceRequest] DataSourceRequest request, int periferiaId)
        {
            var dimoi = db.SYS_DIMOS.Where(o => o.DIMOS_PERIFERIA == periferiaId).Select(p => new DimosViewModel()
            {
                DIMOS_ID = p.DIMOS_ID,
                DIMOS = p.DIMOS,
                DIMOS_PERIFERIA = p.DIMOS_PERIFERIA
            });
            return Json(dimoi.ToDataSourceResult(request));
        }

        public ActionResult DimoiPrint()
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
                return View();
            }
        }

        #endregion

    }
}