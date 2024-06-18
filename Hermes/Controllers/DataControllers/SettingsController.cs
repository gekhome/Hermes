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
using Hermes.Filters;
using Hermes.BPM;
using Hermes.Notification;
using Hermes.Services;

namespace Hermes.Controllers.DataControllers
{
    [ErrorHandlerFilter]
    public class SettingsController : ControllerUnit
    {
        private readonly HermesDBEntities db;

        private readonly ISchoolYearService schoolYearService;
        private readonly IProsklisiService prosklisiService;
        private readonly IFamilyStatusService familyStatusService;
        private readonly INationalityService nationalityService;
        private readonly IJobSectorService jobSectorService;
        private readonly IUserParentService userParentService;

        public SettingsController(HermesDBEntities entities, ISchoolYearService schoolYearService,
            IProsklisiService prosklisiService, IFamilyStatusService familyStatusService, INationalityService nationalityService,
            IJobSectorService jobSectorService, IUserParentService userParentService) : base(entities)
        {
            db = entities;

            this.schoolYearService = schoolYearService;
            this.prosklisiService = prosklisiService;
            this.familyStatusService = familyStatusService;
            this.nationalityService = nationalityService;
            this.jobSectorService = jobSectorService;
            this.userParentService = userParentService;
        }


        #region SCHOOL YEARS

        public ActionResult SchoolYearsList()
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

            return View();
        }

        public ActionResult SchoolYear_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<SchoolYearsViewModel> data = schoolYearService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΠΡΟΣΚΛΗΣΕΙΣ (READ-ONLY)

        public ActionResult ProsklisisList()
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


        #endregion


        #region ΕΘΝΙΚΟΤΗΤΕΣ

        public ActionResult NationalitiesList()
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
            return View();
        }

        public ActionResult Nationality_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<NationalityViewModel> data = nationalityService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        #endregion


        #region ΤΟΜΕΙΣ ΕΠΑΓΓΕΛΜΑΤΟΣ

        public ActionResult JobSectorsList()
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
            return View();
        }

        public ActionResult JobSector_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<JobSectorViewModel> data = jobSectorService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΟΙΚΟΓΕΝΕΙΑΚΗ ΚΑΤΑΣΤΑΣΗ

        public ActionResult FamilyStatusList()
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
            return View();
        }

        public ActionResult FamilyStatus_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<FamilyStatusViewModel> data = familyStatusService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        #endregion


        #region ΛΟΓΑΡΙΑΣΜΟΙ ΑΙΤΟΥΝΤΩΝ ΓΟΝΕΩΝ

        public ActionResult UserParentsList()
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

            PopulateParentTypes();
            return View();
        }


        #region ACCOUNT GRID CRUD Functions

        public ActionResult UserParent_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<UserParentEditViewModel> data = userParentService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region INFO GRID - ΣΤΟΙΧΕΙΑ ΑΙΤΟΥΝΤΟΣ ΓΟΝΕΑ

        public ActionResult UserParentInfo_Read([DataSourceRequest] DataSourceRequest request, int userId = 0)
        {
            IEnumerable<ParentAccountInfoViewModel> data = userParentService.Detail(userId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult ParentAccountsPrint()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_STATIONS");
            }
            else
            {
                int stationId = (int)GetLoginStation().STATION_ID;
                StationProsklisiParameters parameters = new StationProsklisiParameters();
                parameters.STATION_ID = stationId;

                return View(parameters);
            }
        }


        #endregion


        #region ΠΕΡΙΦΕΡΕΙΕΣ-ΔΗΜΟΙ

        public ActionResult PeriferiesDimoi()
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

        public ActionResult PeriferiesPrint()
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
            return View();
        }

        #endregion

    }
}