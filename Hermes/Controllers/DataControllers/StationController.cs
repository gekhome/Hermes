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
using Hermes.Filters;
using Hermes.Notification;
using Hermes.Services;


namespace Hermes.Controllers.DataControllers
{
    [ErrorHandlerFilter]
    public class StationController : ControllerUnit
    {
        private readonly HermesDBEntities db;

        private readonly IParentService parentService;
        private readonly IStatementService statementService;
        private readonly IAitisisMoriaService aitisisMoriaService;
        private readonly IAitisisEditService aitisisEditService;
        private readonly IRegistryParentService registryParentService;
        private readonly IRegistryStatementService registryStatementService;
        private readonly IRegistryChildrenService registryChildrenService;

        public StationController(HermesDBEntities entities, IParentService parentService, IStatementService statementService,
            IAitisisMoriaService aitisisMoriaService, IAitisisEditService aitisisEditService, IRegistryParentService registryParentService,
            IRegistryStatementService registryStatementService, IRegistryChildrenService registryChildrenService) : base(entities)
        {
            db = entities;
            this.parentService = parentService;
            this.statementService = statementService;
            this.aitisisMoriaService = aitisisMoriaService;
            this.aitisisEditService = aitisisEditService;
            this.registryParentService = registryParentService;
            this.registryStatementService = registryStatementService;
            this.registryChildrenService = registryChildrenService;
        }


        public ActionResult Index(string notify = null)
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
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);
            return View();
        }


        #region ΑΙΤΗΣΕΙΣ ΣΤΑΘΜΟΥ

        public ActionResult AitiseisList(string notify = null)
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
            int schoolYearId = (int)Common.GetActiveProsklisi().SCHOOL_YEAR;
            ViewData["prosklisiProtocol"] = Common.GetActiveProsklisi().PROTOCOL;
            ViewData["schoolYearText"] = Common.GetSchoolYearText(schoolYearId);

            if (prosklisiId == 0)
            {
                string Msg = "Δεν βρέθηκε ενεργή Πρόσκληση.";
                return RedirectToAction("Index", "Station", new { notify = Msg });
            }
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            PopulateAgeClasses();
            return View();
        }


        public ActionResult Aitiseis_Read([DataSourceRequest] DataSourceRequest request)
        {
            int prosklisiId = Common.GetActiveProsklisiID();
            int stationId = (int)GetLoginStation().STATION_ID;

            IEnumerable<AitiseisListViewModel> data = aitisisMoriaService.Read(prosklisiId, stationId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
      
        public PartialViewResult GetParentsRecord(int parentsId)
        {
            ParentsInfoViewModel data = aitisisMoriaService.Detail(parentsId);

            return PartialView("ParentsInfoPartial", data);
        }


        public ActionResult AitisiPrint(int aitisiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_STATIONS");
            }
            else
            {
                AitisiParameters parameters = new AitisiParameters();
                parameters.AITISI_ID = aitisiId;
                return View(parameters);
            }
        }

        public ActionResult AitisiStatementPrint(int parentsId, int prosklisiId)
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
            int statementId = Common.GetStatementIdFromParent(prosklisiId, parentsId);
            var data = (from d in db.STATEMENTS
                        where d.STATEMENT_ID == statementId
                        select new StatementViewModel
                        {
                            STATEMENT_ID = d.STATEMENT_ID,
                            PROSKLISI_ID = d.PROSKLISI_ID,
                            PARENTS_ID = d.PARENTS_ID
                        }).FirstOrDefault();
            return View(data);
        }

        public ActionResult AitisiUploadedFiles(int aitisiId = 0, string notify = null)
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

            sqlAITISEIS_LIST SelectedAitisi = (from d in db.sqlAITISEIS_LIST where d.AITISI_ID == aitisiId select d).FirstOrDefault();
            if (SelectedAitisi == null)
            {
                string errMsg = "Δεν υπάρχει επιλεγμένη αίτηση. Κλείστε την καρτέλα και δοκιμάστε πάλι.";
                return RedirectToAction("AitiseisList", "Station", new { notify = errMsg });
            }

            ViewBag.SelectedAitisiData = SelectedAitisi;
            ViewData["aitisi_id"] = SelectedAitisi.AITISI_ID;

            return View();
        }

        public ActionResult AitiseisUploads_Read([DataSourceRequest] DataSourceRequest request, int aitisiId = 0)
        {
            IEnumerable<sqlUploadedFilesViewModel> data = aitisisMoriaService.ReadFiles(aitisiId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΜΟΡΙΟΔΟΤΗΣΗ ΑΙΤΗΣΕΩΝ

        public ActionResult AitisiMoria(int aitisiId)
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

            int schoolYearId = (int)Common.GetActiveProsklisi().SCHOOL_YEAR;
            ViewData["prosklisiProtocol"] = Common.GetActiveProsklisi().PROTOCOL;
            ViewData["schoolYearText"] = Common.GetSchoolYearText(schoolYearId);

            Moria.ComputeAitisiMoria(aitisiId);

            ChildInfoViewModel child_info = aitisisMoriaService.GetChildDetail(aitisiId);
            AitisiCheckViewModel data = aitisisMoriaService.GetAitisi(aitisiId);

            ViewBag.childInfo = child_info;

            return View(data);
        }

        [HttpPost]
        public ActionResult AitisiMoria(int aitisiId, AitisiCheckViewModel model)
        {
            AitisiCheckViewModel data = aitisisMoriaService.GetAitisi(aitisiId);
            ChildInfoViewModel child_info = aitisisMoriaService.GetChildDetail(aitisiId);
            ViewBag.childInfo = child_info;

            string errorMsg = Kerberos.ValidateAitisiMoria(model);
            if (!string.IsNullOrEmpty(errorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + errorMsg);
                return View(data);
            }

            if (model != null && ModelState.IsValid)
            {
                AITISIS entity = aitisisMoriaService.AuditAitisi(model, aitisiId);

                data.RANKING = entity.RANKING;
                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                return View(data);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω σφαλμάτων καταχώρησης. Ελέγξτε τα μηνύματα στην καρτέλα.");
            return View(data);
        }

        public ActionResult AitisiMoriaPrint(int aitisiId)
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
            AitisiParameters parameters = new AitisiParameters
            {
                AITISI_ID = aitisiId
            };
            return View(parameters);
        }

        public ActionResult BatchMoriodotisi()
        {
            string message = "";
            int prosklisiId = Common.GetActiveProsklisiID();
            int stationId = (int)GetLoginStation().STATION_ID;

            var aitiseis = (from d in db.AITISIS where d.PROSKLISI_ID == prosklisiId && d.STATION_ID == stationId select d).ToList();
            if (aitiseis.Count == 0)
            {
                message = "Δεν βρέθηκαν αιτήσεις του σταθμού για μοριοδότηση.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            foreach (var item in aitiseis)
            {
                Moria.ComputeAitisiMoria(item.AITISI_ID);
            }
            message = "Η μοριοδότηση των αιτήσεων ολοκληρώθηκε.";
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BatchFixAges()
        {
            string message = "";
            int prosklisiId = Common.GetActiveProsklisiID();
            int stationId = (int)GetLoginStation().STATION_ID;

            var aitiseis = (from d in db.AITISIS where d.PROSKLISI_ID == prosklisiId && d.STATION_ID == stationId select d).ToList();
            if (aitiseis.Count == 0)
            {
                message = "Δεν βρέθηκαν αιτήσεις του σταθμού για διόρθωση ηλικιών.";
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            foreach (var item in aitiseis)
            {
                Common.UpdateAitisiChildAge((int)item.CHILD_ID);
            }
            message = "Η διόρθωση ηλικιών στις αιτήσεις ολοκληρώθηκε.";
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΕΠΕΞΕΡΓΑΣΙΑ ΑΙΤΗΣΕΩΝ

        public ActionResult AitiseisData(string notify = null)
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

            int stationId = (int)loggedStation.STATION_ID;
            int prosklisiId = Common.GetActiveProsklisiID();
            int schoolYearId = (int)Common.GetActiveProsklisi().SCHOOL_YEAR;
            ViewData["prosklisiProtocol"] = Common.GetActiveProsklisi().PROTOCOL;
            ViewData["schoolYearText"] = Common.GetSchoolYearText(schoolYearId);

            if (prosklisiId == 0)
            {
                string Msg = "Δεν βρέθηκε ενεργή Πρόσκληση.";
                return RedirectToAction("Index", "Station", new { notify = Msg });
            }
            if (!Kerberos.ChildrenStationExist(prosklisiId, stationId))
            {
                string Msg = "Δεν βρέθηκαν καταχωρημένα παιδιά για τον σταθμό αυτό και αυτή την πρόσκληση.";
                return RedirectToAction("Index", "Station", new { notify = Msg });
            }

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            PopulateChildren(prosklisiId, stationId);
            PopulateStations();
            return View();
        }

        public ActionResult Aitisi_Read([DataSourceRequest] DataSourceRequest request)
        {
            int stationId = (int)GetLoginStation().STATION_ID;
            int prosklisiId = Common.GetActiveProsklisiID();

            IEnumerable<AitisiViewModel> data = aitisisEditService.Read(prosklisiId, stationId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Aitisi_Update([DataSourceRequest] DataSourceRequest request, AitisiViewModel data)
        {
            AitisiViewModel newdata = new AitisiViewModel();

            if (data != null && ModelState.IsValid)
            {
                aitisisEditService.Update(data);
                newdata = aitisisEditService.Refresh(data.AITISI_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Aitisi_Delete(int aitisiId = 0)
        {
            string msg = aitisisEditService.Delete(aitisiId);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΜΗΤΡΩΟ ΓΟΝΕΩΝ

        public ActionResult StationParents(string notify = null)
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
            int schoolYearId = (int)Common.GetActiveProsklisi().SCHOOL_YEAR;
            ViewData["prosklisiProtocol"] = Common.GetActiveProsklisi().PROTOCOL;
            ViewData["schoolYearText"] = Common.GetSchoolYearText(schoolYearId);

            if (prosklisiId == 0)
            {
                string Msg = "Δεν βρέθηκε ενεργή Πρόσκληση.";
                return RedirectToAction("Index", "Station", new { notify = Msg });
            }
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            PopulateGenders();
            PopulateAgeClasses();
            return View();
        }

        public ActionResult StationParents_Read([DataSourceRequest] DataSourceRequest request)
        {
            int prosklisiId = Common.GetActiveProsklisiID();
            int stationId = (int)GetLoginStation().STATION_ID;

            IEnumerable<sqlParentGridViewModel> data = registryParentService.ReadParents(prosklisiId, stationId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        // CHILD-GRID WITH AITISEIS
        public ActionResult ParentAitiseis_Read([DataSourceRequest] DataSourceRequest request, int parentsId = 0)
        {
            IEnumerable<sqlAitisiGridViewModel> data = registryParentService.ReadAitisis(parentsId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult StationParentsPrint()
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
            int stationId = (int)loggedStation.STATION_ID;
            int prosklisiId = Common.GetActiveProsklisiID();

            StationProsklisiParameters parameters = new StationProsklisiParameters
            {
                PROSKLISI_ID = prosklisiId,
                STATION_ID = stationId
            };
            return View(parameters);
        }


        #region ΦΟΡΜΑ ΣΤΟΙΧΕΙΩΝ ΓΟΝΕΑ

        public ActionResult ParentsEdit(int parentsId)
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
                string Msg = "Δεν βρέθηκε ενεργή Πρόσκληση.";
                return RedirectToAction("Index", "Station", new { notify = Msg });
            }

            ParentsViewModel data = registryParentService.GetRecord(parentsId);
            if (data == null)
            {
                string msg = "Προέκυψε σφάλμα ανάκτησης δεδομένω του γονέα.";
                return RedirectToAction("ErrorData", "Station", new { notify = msg });
            }
            return View(data);
        }

        [HttpPost]
        public ActionResult ParentsEdit(int parentsId, ParentsViewModel data)
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

            string ErrorMsg = Kerberos.ValidateParentsFields(data);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(data);
            }

            if (ModelState.IsValid)
            {
                parentService.UpdateRecord(data, parentsId);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                return View(data);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω σφαλμάτων καταχώρησης.");
            return View(data);
        }

        #endregion

        #endregion


        #region ΜΗΤΡΩΟ ΔΗΛΩΣΕΩΝ

        public ActionResult StationStatements(string notify = null)
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
            int schoolYearId = (int)Common.GetActiveProsklisi().SCHOOL_YEAR;
            ViewData["prosklisiProtocol"] = Common.GetActiveProsklisi().PROTOCOL;
            ViewData["schoolYearText"] = Common.GetSchoolYearText(schoolYearId);

            if (prosklisiId == 0)
            {
                string Msg = "Δεν βρέθηκε ενεργή Πρόσκληση.";
                return RedirectToAction("Index", "Station", new { notify = Msg });
            }
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            PopulateJobSectors();
            return View();
        }

        public ActionResult StationStatements_Read([DataSourceRequest] DataSourceRequest request)
        {
            int prosklisiId = Common.GetActiveProsklisiID();
            int stationId = (int)GetLoginStation().STATION_ID;

            IEnumerable<sqlStatementGridViewModel> data = registryStatementService.Read(prosklisiId, stationId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<sqlStatementGridViewModel> GetStationStatementsFromDB()
        {
            int stationId = (int)GetLoginStation().STATION_ID;
            int prosklisiId = Common.GetActiveProsklisiID();

            var data = (from d in db.gridSTATEMENT_DATA
                        where d.STATION_ID == stationId && d.PROSKLISI_ID == prosklisiId
                        orderby d.MOTHER_FULLNAME, d.FATHER_FULLNAME
                        select new sqlStatementGridViewModel
                        {
                            STATEMENT_ID = d.STATEMENT_ID,
                            STATEMENT_DATE = d.STATEMENT_DATE,
                            PARENTS_ID = d.PARENTS_ID,
                            STATION_ID = d.STATION_ID,
                            FATHER_FULLNAME = d.FATHER_FULLNAME,
                            MOTHER_FULLNAME = d.MOTHER_FULLNAME,
                            FATHER_JOBSECTOR = d.FATHER_JOBSECTOR,
                            MOTHER_JOBSECTOR = d.MOTHER_JOBSECTOR,
                            PROSKLISI_ID = d.PROSKLISI_ID,
                            PROTOCOL = d.PROTOCOL,
                            STATION_NAME = d.STATION_NAME,
                            SY_TEXT = d.SY_TEXT
                        }).ToList();

            return (data);
        }

        public ActionResult StatementEdit(int statementId)
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
                string Msg = "Δεν βρέθηκε ενεργή Πρόσκληση.";
                return RedirectToAction("Index", "Station", new { notify = Msg });
            }
            ViewBag.ProsklisiData = Common.GetActiveProsklisi();

            var parent_data = Common.GetParentDataFromStatement(statementId);
            if (parent_data != null)
                ViewBag.ParentData = parent_data;
            else
            {
                string msg = "Προέκυψε σφάλμα ανάκτησης δεδομένων γονέων.";
                return RedirectToAction("ErrorData", "Station", new { notify = msg });
            }

            StatementViewModel data = registryStatementService.GetRecord(statementId);
            return View(data);
        }

        [HttpPost]
        public ActionResult StatementEdit(int statementId, StatementViewModel data)
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
            var statement = (from d in db.STATEMENTS where d.STATEMENT_ID == statementId select d).FirstOrDefault();
            int parentsId = (int)statement.PARENTS_ID;
            int prosklisiId = Common.GetActiveProsklisiID();

            var parent_data = Common.GetParentDataFromStatement(statementId);
            ViewBag.ParentData = parent_data;

            data.INCOME_CATEGORY = Common.ComputeIncomeCategory(data.INCOME_FAMILY);
            string ErrorMsg = Common.VerifyIncomeCategory(data.INCOME_CATEGORY);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(data);
            }

            if (data != null && ModelState.IsValid)
            {
                statementService.UpdateRecord(data, statementId, parentsId, prosklisiId);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                StatementViewModel newdata = statementService.GetRecord(statementId);
                return View(newdata);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω σφαλμάτων καταχώρησης.");
            return View(data);
        }

        public ActionResult StatementPrint(int statementId)
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
            var data = (from d in db.STATEMENTS
                        where d.STATEMENT_ID == statementId
                        select new StatementViewModel
                        {
                            STATEMENT_ID = d.STATEMENT_ID,
                            PROSKLISI_ID = d.PROSKLISI_ID,
                            PARENTS_ID = d.PARENTS_ID
                        }).FirstOrDefault();

            return View(data);
        }


        #endregion


        #region ΜΗΤΡΩΟ ΠΑΙΔΙΩΝ

        public ActionResult StationChildren(string notify = null)
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
            int schoolYearId = (int)Common.GetActiveProsklisi().SCHOOL_YEAR;
            ViewData["prosklisiProtocol"] = Common.GetActiveProsklisi().PROTOCOL;
            ViewData["schoolYearText"] = Common.GetSchoolYearText(schoolYearId);

            if (prosklisiId == 0)
            {
                string Msg = "Δεν βρέθηκε ενεργή Πρόσκληση.";
                return RedirectToAction("Index", "Station", new { notify = Msg });
            }
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            PopulateNationalities();
            PopulateGenders();
            PopulateParents();
            return View();
        }

        public ActionResult Children_Read([DataSourceRequest] DataSourceRequest request)
        {
            int prosklisiId = Common.GetActiveProsklisiID();
            int stationId = (int)GetLoginStation().STATION_ID;

            IEnumerable<sqlChildrenViewModel> data = registryChildrenService.Read(prosklisiId, stationId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Children_Update([DataSourceRequest] DataSourceRequest request, sqlChildrenViewModel data)
        {
            ChildrenViewModel newdata = new ChildrenViewModel();

            if (!Kerberos.ValidBirthDate((DateTime)data.BIRTHDATE))
            {
                ModelState.AddModelError("", "Η ημερομηνία γέννησης είναι εκτός αποδεκτών ορίων. Η καταχώρηση ακυρώθηκε.");
            }

            if (data != null && ModelState.IsValid)
            {
                registryChildrenService.Update(data);
                newdata = registryChildrenService.Refresh(data.CHILD_ID);

                // Update age in aitisi if there is one
                Common.UpdateAitisiChildAge(data.CHILD_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region ΑΙΤΗΣΕΙΣ ΚΑΙ ΔΙΚΑΙΟΛΟΓΗΤΙΚΑ

        public ActionResult AitiseisFiles(string notify = null)
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

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            PopulateAgeClasses();
            return View();
        }

        public ActionResult AitiseisProsklisi_Read([DataSourceRequest] DataSourceRequest request, int prosklisiID = 0)
        {
            var data = GetProsklisiAitiseisFromDB(prosklisiID);

            var result = new JsonResult();
            result.Data = data.ToDataSourceResult(request);
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

        public List<AitiseisListViewModel> GetProsklisiAitiseisFromDB(int prosklisiID)
        {
            int stationId = (int)GetLoginStation().STATION_ID;

            var data = (from d in db.sqlAITISEIS_LIST
                        where d.PROSKLISI_ID == prosklisiID && d.STATION_ID == stationId
                        orderby d.CHILD_FULLNAME, d.AITISI_PROTOCOL
                        select new AitiseisListViewModel
                        {
                            AITISI_ID = d.AITISI_ID,
                            PROSKLISI_ID = d.PROSKLISI_ID,
                            STATION_ID = d.STATION_ID,
                            AITISI_DATE = d.AITISI_DATE,
                            AITISI_PROTOCOL = d.AITISI_PROTOCOL,
                            CHILD_FULLNAME = d.CHILD_FULLNAME,
                            CHILD_ID = d.CHILD_ID,
                            PARENTS_ID = d.PARENTS_ID,
                            STATION_NAME = d.STATION_NAME,
                            AGE = d.AGE,
                            AGE_CATEGORY = d.AGE_CATEGORY,
                            BIRTHDATE = d.BIRTHDATE,
                            GENDER = d.GENDER,
                            MORIA_TOTAL = d.MORIA_TOTAL,
                            RANKING_TEXT = d.RANKING_TEXT
                        }).ToList();

            return (data);
        }



        #endregion


        #region ΠΙΝΑΚΕΣ ΑΞΙΟΛΟΓΗΣΗΣ

        public ActionResult Pinakas1_Print()
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
            PARAMETROI_PINAKES parameters = new PARAMETROI_PINAKES
            {
                PROSKLISI_ID = Common.GetActiveProsklisiID(),
                PERIFERIAKI_ID = (from d in db.SYS_STATIONS where d.ΣΤΑΘΜΟΣ_ΚΩΔ == loggedStation.STATION_ID select d.ΠΕΡΙΦΕΡΕΙΑΚΗ).FirstOrDefault(),
                STATION_ID = loggedStation.STATION_ID
            };
            return View(parameters);
        }

        public ActionResult Pinakas2_Print()
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
            PARAMETROI_PINAKES parameters = new PARAMETROI_PINAKES
            {
                PROSKLISI_ID = Common.GetActiveProsklisiID(),
                PERIFERIAKI_ID = (from d in db.SYS_STATIONS where d.ΣΤΑΘΜΟΣ_ΚΩΔ == loggedStation.STATION_ID select d.ΠΕΡΙΦΕΡΕΙΑΚΗ).FirstOrDefault(),
                STATION_ID = loggedStation.STATION_ID
            };
            return View(parameters);
        }

        public ActionResult Pinakas3_Print()
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
            PARAMETROI_PINAKES parameters = new PARAMETROI_PINAKES
            {
                PROSKLISI_ID = Common.GetActiveProsklisiID(),
                PERIFERIAKI_ID = (from d in db.SYS_STATIONS where d.ΣΤΑΘΜΟΣ_ΚΩΔ == loggedStation.STATION_ID select d.ΠΕΡΙΦΕΡΕΙΑΚΗ).FirstOrDefault(),
                STATION_ID = loggedStation.STATION_ID
            };
            return View(parameters);
        }

        public ActionResult Pinakas0_Print()
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
            PARAMETROI_PINAKES parameters = new PARAMETROI_PINAKES
            {
                PROSKLISI_ID = Common.GetActiveProsklisiID(),
                PERIFERIAKI_ID = (from d in db.SYS_STATIONS where d.ΣΤΑΘΜΟΣ_ΚΩΔ == loggedStation.STATION_ID select d.ΠΕΡΙΦΕΡΕΙΑΚΗ).FirstOrDefault(),
                STATION_ID = loggedStation.STATION_ID
            };
            return View(parameters);
        }


        #endregion


        #region ΕΚΤΥΠΩΣΕΙΣ

        public ActionResult StationAitiseisAgesPrint()
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
            StationProsklisiParameters paramaters = new StationProsklisiParameters
            {
                PROSKLISI_ID = prosklisiId
            };
            return View(paramaters);
        }

        public ActionResult AitiseisNoStatementPrint()
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
            PARAMETROI_PINAKES parameters = new PARAMETROI_PINAKES
            {
                PROSKLISI_ID = Common.GetActiveProsklisiID(),
                PERIFERIAKI_ID = 0,
                STATION_ID = loggedStation.STATION_ID
            };
            return View(parameters);
        }

        public ActionResult EnstaseisSummaryPrint()
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
            PARAMETROI_PINAKES parameters = new PARAMETROI_PINAKES
            {
                PROSKLISI_ID = Common.GetActiveProsklisiID(),
                PERIFERIAKI_ID = 0,
                STATION_ID = 0
            };
            return View(parameters);
        }

        public ActionResult PraktikaEpitropesPrint()
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
            PARAMETROI_PINAKES parameters = new PARAMETROI_PINAKES
            {
                PROSKLISI_ID = Common.GetActiveProsklisiID(),
                PERIFERIAKI_ID = (from d in db.SYS_STATIONS where d.ΣΤΑΘΜΟΣ_ΚΩΔ == loggedStation.STATION_ID select d.ΠΕΡΙΦΕΡΕΙΑΚΗ).FirstOrDefault(),
                STATION_ID = loggedStation.STATION_ID
            };
            return View(parameters);
        }

        public ActionResult StationAitiseisPrint()
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

            StationProsklisiParameters paramaters = new StationProsklisiParameters
            {
                PROSKLISI_ID = prosklisiId
            };
            return View(paramaters);
        }

        public ActionResult MultiChildAitiseisPrint()
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

            PARAMETROI_PINAKES pp = new PARAMETROI_PINAKES
            {
                PROSKLISI_ID = prosklisiId,
                STATION_ID = loggedStation.STATION_ID
            };
            return View(pp);
        }

        public ActionResult DuplicateAitiseisPrint()
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


        #region ΣΤΑΤΙΣΤΚΑ

        public ActionResult AitiseisDailyPrint()
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
                int prosklisiId = Common.GetActiveProsklisiID();
                PARAMETROI_PINAKES pp = new PARAMETROI_PINAKES();
                pp.PROSKLISI_ID = prosklisiId;
                return View(pp);
            }
        }

        public ActionResult Stat_RegisterType()
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
                int prosklisiId = Common.GetActiveProsklisiID();
                PARAMETROI_PINAKES pp = new PARAMETROI_PINAKES();
                pp.PROSKLISI_ID = prosklisiId;
                return View(pp);
            }
        }

        public ActionResult Stat_AgeClasses()
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
                int prosklisiId = Common.GetActiveProsklisiID();
                PARAMETROI_PINAKES pp = new PARAMETROI_PINAKES();
                pp.PROSKLISI_ID = prosklisiId;
                return View(pp);
            }
        }

        public ActionResult Stat_FamilyStatus()
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
                int prosklisiId = Common.GetActiveProsklisiID();
                PARAMETROI_PINAKES pp = new PARAMETROI_PINAKES();
                pp.PROSKLISI_ID = prosklisiId;
                return View(pp);
            }
        }

        public ActionResult Stat_Gender()
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
                int prosklisiId = Common.GetActiveProsklisiID();
                PARAMETROI_PINAKES pp = new PARAMETROI_PINAKES();
                pp.PROSKLISI_ID = prosklisiId;
                return View(pp);
            }
        }

        public ActionResult Stat_JobStatus()
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
                int prosklisiId = Common.GetActiveProsklisiID();
                PARAMETROI_PINAKES pp = new PARAMETROI_PINAKES();
                pp.PROSKLISI_ID = prosklisiId;
                return View(pp);
            }
        }

        public ActionResult Stat_Nationalities()
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
                int prosklisiId = Common.GetActiveProsklisiID();
                PARAMETROI_PINAKES pp = new PARAMETROI_PINAKES();
                pp.PROSKLISI_ID = prosklisiId;
                return View(pp);
            }
        }

        #endregion

    }
}