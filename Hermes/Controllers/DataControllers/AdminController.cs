using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Hermes.DAL;
using Hermes.Filters;
using Hermes.Models;
using Hermes.BPM;
using Hermes.Notification;
using Hermes.Services;

namespace Hermes.Controllers.DataControllers
{
    [ErrorHandlerFilter]
    public class AdminController : ControllerUnit
    {
        private readonly HermesDBEntities db;

        private readonly IParentService parentService;
        private readonly IStatementService statementService;
        private readonly IUserParentService userParentService;
        private readonly IUserStationService userStationService;
        private readonly IAitisisMoriaService aitisisMoriaService;
        private readonly IAitisisEditService aitisisEditService;
        private readonly IRegistryParentService registryParentService;
        private readonly IRegistryStatementService registryStatementService;
        private readonly IRegistryChildrenService registryChildrenService;

        public AdminController(HermesDBEntities entities, IParentService parentService, 
            IStatementService statementService, IUserParentService userParentService, IUserStationService userStationService,
            IAitisisMoriaService aitisisMoriaService, IAitisisEditService aitisisEditService, IRegistryParentService registryParentService, 
            IRegistryStatementService registryStatementService, IRegistryChildrenService registryChildrenService) : base(entities)
        {
            db = entities;

            this.parentService = parentService;
            this.statementService = statementService;
            this.userParentService = userParentService;
            this.userStationService = userStationService;
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
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            if (notify != null)
            {
                this.ShowMessage(MessageType.Warning, notify);
            }
            return View();
        }


        #region ΑΙΤΗΣΕΙΣ ΓΟΝΕΩΝ

        public ActionResult AdminAitiseisList(string notify = null)
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

            if (prosklisiId == 0)
            {
                string Msg = "Δεν βρέθηκε ενεργή Πρόσκληση.";
                return RedirectToAction("Index", "Admin", new { notify = Msg });
            }
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            PopulateAgeClasses();
            return View();
        }

        public ActionResult Aitiseis_Read([DataSourceRequest] DataSourceRequest request)
        {
            int prosklisiId = Common.GetActiveProsklisiID();

            IEnumerable<AitiseisListViewModel> data = aitisisMoriaService.Read(prosklisiId);

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
                return RedirectToAction("Login", "USER_ADMINS");
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
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
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

        public ActionResult AdminAitisiFiles(int aitisiId = 0, string notify = null)
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

            sqlAITISEIS_LIST SelectedAitisi = (from d in db.sqlAITISEIS_LIST where d.AITISI_ID == aitisiId select d).FirstOrDefault();
            if (SelectedAitisi == null)
            {
                string errMsg = "Δεν υπάρχει επιλεγμένη αίτηση. Κλείστε την καρτέλα και δοκιμάστε πάλι.";
                return RedirectToAction("AdminAitiseisList", "Admin", new { notify = errMsg });
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
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            AitisiParameters parameters = new AitisiParameters();
            parameters.AITISI_ID = aitisiId;
            return View(parameters);
        }

        public ActionResult BatchMoriodotisi()
        {
            string message = "";
            int prosklisiId = Common.GetActiveProsklisiID();

            var aitiseis = (from d in db.AITISIS where d.PROSKLISI_ID == prosklisiId orderby d.STATION_ID select d).ToList();
            if (aitiseis.Count == 0)
            {
                message = "Δεν βρέθηκαν αιτήσεις στην Πρόσκληση αυτή για μοριοδότηση.";
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

            var aitiseis = (from d in db.AITISIS where d.PROSKLISI_ID == prosklisiId orderby d.STATION_ID select d).ToList();
            if (aitiseis.Count == 0)
            {
                message = "Δεν βρέθηκαν αιτήσεις στην Πρόσκληση αυτή για διόρθωση ηλικιών.";
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

        public ActionResult AdminAitiseisData(string notify = null)
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

            if (prosklisiId == 0)
            {
                string Msg = "Δεν βρέθηκε ενεργή Πρόσκληση.";
                return RedirectToAction("Index", "Admin", new { notify = Msg });
            }
            if (!Kerberos.ChildrenExist(prosklisiId))
            {
                string Msg = "Δεν βρέθηκαν καταχωρημένα παιδιά για την πρόσκληση αυτή.";
                return RedirectToAction("Index", "Admin", new { notify = Msg });
            }

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            PopulateChildren(prosklisiId);
            PopulateStations();
            return View();
        }

        public ActionResult Aitisi_Read([DataSourceRequest] DataSourceRequest request)
        {
            int prosklisiId = Common.GetActiveProsklisiID();

            IEnumerable<AitisiViewModel> data = aitisisEditService.Read(prosklisiId);

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

        public ActionResult AdminParents(string notify = null)
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

            if (prosklisiId == 0)
            {
                string Msg = "Δεν βρέθηκε ενεργή Πρόσκληση.";
                return RedirectToAction("Index", "Admin", new { notify = Msg });
            }
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            PopulateGenders();
            PopulateStations();
            PopulateAgeClasses();
            return View();
        }

        public ActionResult AdminParents_Read([DataSourceRequest] DataSourceRequest request)
        {
            int prosklisiId = Common.GetActiveProsklisiID();

            IEnumerable<sqlParentGridViewModel> data = registryParentService.ReadParents(prosklisiId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        // CHILD-GRID WITH AITISEIS
        public ActionResult ParentAitiseis_Read([DataSourceRequest] DataSourceRequest request, int parentsId = 0)
        {
            IEnumerable<sqlAitisiGridViewModel> data = registryParentService.ReadAitisis(parentsId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AdminParentsPrint()
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
            StationProsklisiParameters parameters = new StationProsklisiParameters
            {
                PROSKLISI_ID = prosklisiId,
                STATION_ID = 0
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

            ParentsViewModel data = registryParentService.GetRecord(parentsId);
            if (data == null)
            {
                string msg = "Προέκυψε σφάλμα ανάκτησης δεδομένω του γονέα.";
                return RedirectToAction("ErrorData", "Admin", new { notify = msg });
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
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
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

        public ActionResult AdminStatements(string notify = null)
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

            if (prosklisiId == 0)
            {
                string Msg = "Δεν βρέθηκε ενεργή Πρόσκληση.";
                return RedirectToAction("Index", "Admin", new { notify = Msg });
            }
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            PopulateJobSectors();
            return View();
        }

        public ActionResult AdminStatements_Read([DataSourceRequest] DataSourceRequest request)
        {
            int prosklisiId = Common.GetActiveProsklisiID();

            IEnumerable<sqlStatementGridViewModel> data = registryStatementService.Read(prosklisiId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult StatementEdit(int statementId)
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
            ViewBag.ProsklisiData = Common.GetActiveProsklisi();

            var parent_data = Common.GetParentDataFromStatement(statementId);
            if (parent_data != null)
                ViewBag.ParentData = parent_data;
            else
            {
                string msg = "Προέκυψε σφάλμα ανάκτησης δεδομένων γονέων.";
                return RedirectToAction("ErrorData", "Admin", new { notify = msg });
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
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
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
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
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

        public ActionResult AdminChildren(string notify = null)
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

            if (prosklisiId == 0)
            {
                string Msg = "Δεν βρέθηκε ενεργή Πρόσκληση.";
                return RedirectToAction("Index", "Admin", new { notify = Msg });
            }
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            PopulateNationalities();
            PopulateGenders();
            PopulateParents();
            PopulateStations();
            return View();
        }

        public ActionResult Children_Read([DataSourceRequest] DataSourceRequest request)
        {
            int prosklisiId = Common.GetActiveProsklisiID();

            IEnumerable<sqlChildrenViewModel> data = registryChildrenService.Read(prosklisiId);

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


        #region ΠΙΝΑΚΕΣ ΑΞΙΟΛΟΓΗΣΗΣ

        public ActionResult Pinakas1_Print()
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
            PARAMETROI_PINAKES parameters = new PARAMETROI_PINAKES
            {
                PROSKLISI_ID = Common.GetActiveProsklisiID(),
                PERIFERIAKI_ID = 0,
                STATION_ID = 0
            };
            return View(parameters);
        }

        public ActionResult Pinakas2_Print()
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
            PARAMETROI_PINAKES parameters = new PARAMETROI_PINAKES
            {
                PROSKLISI_ID = Common.GetActiveProsklisiID(),
                PERIFERIAKI_ID = 0,
                STATION_ID = 0
            };
            return View(parameters);
        }

        public ActionResult Pinakas3_Print()
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
            PARAMETROI_PINAKES parameters = new PARAMETROI_PINAKES
            {
                PROSKLISI_ID = Common.GetActiveProsklisiID(),
                PERIFERIAKI_ID = 0,
                STATION_ID = 0
            };
            return View(parameters);
        }

        public ActionResult Pinakas0_Print()
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
            PARAMETROI_PINAKES parameters = new PARAMETROI_PINAKES
            {
                PROSKLISI_ID = Common.GetActiveProsklisiID(),
                PERIFERIAKI_ID = 0,
                STATION_ID = 0
            };
            return View(parameters);
        }

        #endregion


        #region ΛΟΓΑΡΙΑΣΜΟΙ ΑΙΤΟΥΝΤΩΝ ΓΟΝΕΩΝ

        public ActionResult ListUserParents()
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

            PopulateParentTypes();
            return View();
        }

        #region ACCOUNT GRID CRUD Functions

        [HttpPost]
        public ActionResult UserParent_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<UserParentEditViewModel> data = userParentService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UserParent_Create([DataSourceRequest] DataSourceRequest request, UserParentEditViewModel data)
        {
            UserParentEditViewModel newdata = new UserParentEditViewModel();

            if (data != null && ModelState.IsValid)
            {
                userParentService.Create(data);
                newdata = userParentService.Refresh(data.USER_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UserParent_Update([DataSourceRequest] DataSourceRequest request, UserParentEditViewModel data)
        {
            var newdata = new UserParentEditViewModel();

            if (data != null && ModelState.IsValid)
            {
                userParentService.Update(data);
                newdata = userParentService.Refresh(data.USER_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UserParent_Destroy([DataSourceRequest] DataSourceRequest request, UserParentEditViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteUserAccount(data.USER_ID))
                {
                    userParentService.Destroy(data);
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region INFO GRID - ΣΤΟΙΧΕΙΑ ΑΙΤΟΥΝΤΟΣ ΓΟΝΕΑ

        public ActionResult UserParentInfo_Read([DataSourceRequest] DataSourceRequest request, int userId = 0)
        {
            IEnumerable<ParentAccountInfoViewModel> data = userParentService.Detail(userId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion


        #region ΛΟΓΑΡΙΑΣΜΟΙ ΒΡΕΦΟΝΗΠΙΑΚΩΝ ΣΤΑΘΜΩΝ

        public ActionResult ListUserStations(string notify = null)
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

            PopulateStations();

            if (notify != null) this.ShowMessage(MessageType.Info, notify);
            return View();
        }

        public ActionResult CreatePasswords()
        {
            var stations = (from s in db.USER_STATIONS orderby s.USERNAME select s).ToList();

            foreach(var station in stations)
            {
                station.PASSWORD = Common.GeneratePassword() + string.Format("{0:000}", station.STATION_ID);

                db.Entry(station).State = EntityState.Modified;
                db.SaveChanges();
            }
            
            string notify = "Η δημιουργία νέων κωδικών των σταθμών ολοκληρώθηκε.";
            return RedirectToAction("ListUserStations", "Admin", new { notify });
        }


        #region STATION ACCOUNTS GRID CRUD Functions

        [HttpPost]
        public ActionResult UserStation_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<UserStationViewModel> data = userStationService.Read();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserStation_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<UserStationViewModel> data)
        {
            var results = new List<UserStationViewModel>();

            if (data != null && ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    userStationService.Create(item);
                    results.Add(item);
                }
            }
            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UserStation_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<UserStationViewModel> data)
        {
            if (data != null && ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    userStationService.Update(item);
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UserStation_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<UserStationViewModel> data)
        {
            if (data.Any())
            {
                foreach (var item in data)
                {
                    if (Kerberos.CanDeleteUserStation((int)item.STATION_ID))
                    {
                        userStationService.Destroy(item);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Πρέπει πρώτα να γίνει διαγραφή του βρεφονηπιακού σταθμού. Η διαγραφή ακυρώθηκε.");
                    }
                }
            }
            return Json(data.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #endregion


        #region ΕΚΤΥΠΩΣΕΙΣ

        public ActionResult StationAitiseisAgesPrint()
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
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            PARAMETROI_PINAKES parameters = new PARAMETROI_PINAKES
            {
                PROSKLISI_ID = Common.GetActiveProsklisiID(),
                PERIFERIAKI_ID = 0,
                STATION_ID = 0
            };
            return View(parameters);
        }

        public ActionResult EnstaseisSummaryPrint()
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
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
            }
            PARAMETROI_PINAKES parameters = new PARAMETROI_PINAKES
            {
                PROSKLISI_ID = Common.GetActiveProsklisiID(),
                PERIFERIAKI_ID = 0,
                STATION_ID = 0
            };
            return View(parameters);
        }

        public ActionResult StationAitiseisPrint()
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
                int prosklisiId = Common.GetActiveProsklisiID();

                StationProsklisiParameters paramaters = new StationProsklisiParameters();
                paramaters.PROSKLISI_ID = prosklisiId;

                return View(paramaters);
            }
        }

        public ActionResult StationAccountsPrint()
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

        public ActionResult MultiChildAitiseisPrint()
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
                int prosklisiId = Common.GetActiveProsklisiID();

                PARAMETROI_PINAKES pp = new PARAMETROI_PINAKES();
                pp.PROSKLISI_ID = prosklisiId;
                return View(pp);
            }
        }
 
        public ActionResult DuplicateAitiseisPrint()
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
   

        public ActionResult ParentAccountsPrint()
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

        public ActionResult ParentsEmailsPrint()
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
                int prosklisiId = Common.GetActiveProsklisiID();

                StationProsklisiParameters paramaters = new StationProsklisiParameters();
                paramaters.PROSKLISI_ID = prosklisiId;

                return View(paramaters);
            }
        }

        #endregion


        #region ΣΤΑΤΙΣΤΚΑ

        public ActionResult AitiseisDailyPrint()
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
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
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
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
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
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
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
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
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
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
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
                return RedirectToAction("Login", "USER_ADMINS");
            }
            else
            {
                loggedAdmin = GetLoginAdmin();
                int prosklisiId = Common.GetActiveProsklisiID();
                PARAMETROI_PINAKES pp = new PARAMETROI_PINAKES();
                pp.PROSKLISI_ID = prosklisiId;
                return View(pp);
            }
        }

        #endregion


        #region Local Functions

        public ActionResult UpdateAitisiReRegistration()
        {
            string msg = "Η ενημέρωση του πεδίου επανεγγραφής των αιτήσεων ολοκληρώθηκε.";

            int prosklisiId = Common.GetActiveProsklisiID();

            var source = (from d in db.rep_AITISI_INFO_MORIA where d.PROSKLISI_ID == prosklisiId select d).ToList();

            foreach (var item in source)
            {
                AITISIS entity = db.AITISIS.Find(item.AITISI_ID);
                if (entity != null)
                {
                    entity.RE_REGISTRATION = item.RE_REGISTRATION;
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            // set null values of RE_REGISTRATION field to false
            var data = (from d in db.AITISIS where d.PROSKLISI_ID == prosklisiId && d.RE_REGISTRATION == null select d).ToList();

            foreach (var item in data)
            {
                AITISIS entity = db.AITISIS.Find(item.AITISI_ID);
                if (entity != null)
                {
                    entity.RE_REGISTRATION = false;
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}