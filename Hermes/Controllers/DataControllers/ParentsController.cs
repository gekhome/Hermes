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
using Hermes.Models;
using Hermes.BPM;
using Hermes.Filters;
using Hermes.Notification;
using Hermes.Services;

namespace Hermes.Controllers.DataControllers
{
    [ErrorHandlerFilter]
    public class ParentsController : ControllerUnit
    {
        private readonly HermesDBEntities db;

        private const string UPLOAD_PATH = "~/Uploads/";

        private readonly IParentService parentService;
        private readonly IStatementService statementService;
        private readonly IAitisisMoriaService aitisisMoriaService;
        private readonly IChildrenService childrenService;
        private readonly IAitisiService aitisiService;
        private readonly IUploadService uploadService;

        public ParentsController(HermesDBEntities entities, 
            IParentService parentService, IStatementService statementService, 
            IAitisisMoriaService aitisisMoriaService, IChildrenService childrenService, 
            IAitisiService aitisiService, IUploadService uploadService) : base(entities)
        {
            db = entities;

            this.parentService = parentService;
            this.statementService = statementService;
            this.aitisisMoriaService = aitisisMoriaService;
            this.childrenService = childrenService;
            this.aitisiService = aitisiService;
            this.uploadService = uploadService;
        }


        public ActionResult Index(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("TaxisNetLogin", "USER_PARENTS");
            }
            else
            {
                loggedParent = GetLoginParent();
            }

            int prosklisiId = Common.GetOpenProsklisiID();
            if (prosklisiId == 0)
            {
                string Msg = "Δεν βρέθηκε ανοικτή Πρόσκληση. Όλες οι ενέργειες δημιουργίας και επεξεργασίας δεδομένων είναι απενεργοποιημένες.";
                this.ShowMessage(MessageType.Warning, Msg);
            }
            if (notify != null)
            {
                this.ShowMessage(MessageType.Warning, notify);
            }
            return View();
        }


        #region ΦΟΡΜΑ ΣΤΟΙΧΕΙΩΝ ΓΟΝΕΩΝ

        public ActionResult ParentsCreate()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("TaxisNetLogin", "USER_PARENTS");
            }
            else
            {
                loggedParent = GetLoginParent();
            }

            int prosklisiId = Common.GetOpenProsklisiID();
            if (prosklisiId == 0)
            {
                return RedirectToAction("Index", "Parents");
            }

            var data = (from d in db.PARENTS where d.PARENT_USERID == loggedParent.USER_ID select d).FirstOrDefault();
            if (data == null)
            {
                if (loggedParent.PARENT_TYPE == 1)
                    return View(new ParentsViewModel() { FATHER_AFM = loggedParent.USER_AFM, PARENT_USERID = loggedParent.USER_ID });
                else
                    return View(new ParentsViewModel() { MOTHER_AFM = loggedParent.USER_AFM, PARENT_USERID = loggedParent.USER_ID });
            }
            else
            {
                return RedirectToAction("ParentsEdit", "Parents");
            }
        }

        [HttpPost]
        public ActionResult ParentsCreate(ParentsViewModel data)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("TaxisNetLogin", "USER_PARENTS");
            }
            else
            {
                loggedParent = GetLoginParent();
            }
            int userId = loggedParent.USER_ID;

            string ErrorMsg = Kerberos.ValidateParentsFields(data);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                if (loggedParent.PARENT_TYPE == 1)
                    data.FATHER_AFM = loggedParent.USER_AFM;
                else
                    data.MOTHER_AFM = loggedParent.USER_AFM;
                return View(data);
            }

            if (data != null && ModelState.IsValid)
            {
                parentService.CreateRecord(data, userId);

                string msg = "Η αποθήκευση ολοκληρώθηκε με επιτυχία.";
                return RedirectToAction("ParentsEdit", "Parents", new { notify = msg });
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω σφαλμάτων καταχώρησης.");
            if (loggedParent.PARENT_TYPE == 1)
                data.FATHER_AFM = loggedParent.USER_AFM;
            else
                data.MOTHER_AFM = loggedParent.USER_AFM;
            return View(data);
        }

        public ActionResult ParentsEdit(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("TaxisNetLogin", "USER_PARENTS");
            }
            else
            {
                loggedParent = GetLoginParent();
            }

            int prosklisiId = Common.GetOpenProsklisiID();
            if (prosklisiId == 0)
            {
                string Msg = "Δεν βρέθηκε ανοικτή Πρόσκληση. Όλες οι ενέργειες δημιουργίας και επεξεργασίας δεδομένων είναι απενεργοποιημένες.";
                return RedirectToAction("Index", "Parents", new { notify = Msg });
            }

            if (notify != null) this.ShowMessage(MessageType.Success, notify);

            ParentsViewModel data = parentService.GetRecord(loggedParent.USER_ID);
            if (data == null)
            {
                return RedirectToAction("ParentsCreate", "Parents");
            }
            return View(data);
        }

        [HttpPost]
        public ActionResult ParentsEdit(ParentsViewModel data)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("TaxisNetLogin", "USER_PARENTS");
            }
            else
            {
                loggedParent = GetLoginParent();
            }
            int userId = loggedParent.USER_ID;
            int parentsId = Common.GetParentIdFromUserId(loggedParent.USER_ID);

            string ErrorMsg = Kerberos.ValidateParentsFields(data);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(data);
            }

            if (ModelState.IsValid)
            {
                parentService.UpdateRecord(data, parentsId, userId);

                this.ShowMessage(MessageType.Success, "Η αποθήκευση ολοκληρώθηκε με επιτυχία.");
                return View(data);
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω σφαλμάτων καταχώρησης.");
            return View(data);
        }

        #endregion


        #region ΠΛΕΓΜΑ ΚΑΙ ΦΟΡΜΑ ΣΤΟΙΧΕΙΩΝ ΠΑΙΔΙΩΝ

        public ActionResult ChildrenData(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("TaxisNetLogin", "USER_PARENTS");
            }
            else
            {
                loggedParent = GetLoginParent();
            }
            int prosklisiId = Common.GetOpenProsklisiID();
            if (prosklisiId == 0)
            {
                string Msg = "Δεν βρέθηκε ανοικτή Πρόσκληση. Όλες οι ενέργειες δημιουργίας και επεξεργασίας δεδομένων είναι απενεργοποιημένες.";
                return RedirectToAction("Index", "Parents", new { notify = Msg });
            }

            if (!ParentsExist(loggedParent.USER_AFM))
            {
                string msg = "Για να καταχωρήσετε τα στοιχεία παιδιών πρέπει πρώτα να έχετε καταχωρήσει στοιχεία γονέων.";
                return RedirectToAction("Index", "Parents", new { notify = msg });
            }

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            PopulateNationalities();
            PopulateGenders();
            return View();
        }

        #region CHILDREN GRID CRUD FUNCTIONS

        public ActionResult Child_Read([DataSourceRequest] DataSourceRequest request)
        {
            loggedParent = GetLoginParent();
            int parentsId = Common.GetParentIdFromUserId(loggedParent.USER_ID);

            IEnumerable<ChildrenViewModel> data = childrenService.Read(parentsId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Child_Create([DataSourceRequest] DataSourceRequest request, ChildrenViewModel data)
        {
            loggedParent = GetLoginParent();
            int parentsId = Common.GetParentIdFromUserId(loggedParent.USER_ID);

            ChildrenViewModel newdata = new ChildrenViewModel();

            if (Kerberos.IsDuplicateChild(parentsId, data.AMKA))
            {
                ModelState.AddModelError("", "Υπάρχει ήδη παιδί με αυτό το ΑΜΚΑ. Η καταχώρηση ακυρώθηκε.");
            }
            if (!Kerberos.ValidBirthDate((DateTime)data.BIRTHDATE))
            {
                ModelState.AddModelError("", "Η ημερομηνία γέννησης είναι εκτός αποδεκτών ορίων. Η καταχώρηση ακυρώθηκε.");
            }
            if (data != null && ModelState.IsValid)
            {
                childrenService.Create(data, parentsId);
                newdata = childrenService.Refresh(data.CHILD_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Child_Update([DataSourceRequest] DataSourceRequest request, ChildrenViewModel data)
        {
            loggedParent = GetLoginParent();
            int parentsId = Common.GetParentIdFromUserId(loggedParent.USER_ID);

            ChildrenViewModel newdata = new ChildrenViewModel();

            if (!Kerberos.ValidBirthDate((DateTime)data.BIRTHDATE))
            {
                ModelState.AddModelError("", "Η ημερομηνία γέννησης είναι εκτός αποδεκτών ορίων. Η καταχώρηση ακυρώθηκε.");
            }

            if (data != null && ModelState.IsValid)
            {
                childrenService.Update(data, parentsId);
                newdata = childrenService.Refresh(data.CHILD_ID);
                // Update age in aitisi if there is one
                Common.UpdateAitisiChildAge(data.CHILD_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Child_Destroy([DataSourceRequest] DataSourceRequest request, ChildrenViewModel data)
        {
            if (!Kerberos.CanDeleteChildren(data.CHILD_ID))
                ModelState.AddModelError("", "Δεν μπορεί να διαγραφεί το παιδί διότι χρησιμοποιείται ήδη σε αιτήσεις.");

            if (ModelState.IsValid)
            {
                childrenService.Destroy(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion


        #region ΔΗΛΩΣΗ ΣΤΟΙΧΕΙΩΝ ΓΟΝΕΩΝ

        public ActionResult StatementCreate()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("TaxisNetLogin", "USER_PARENTS");
            }
            else
            {
                loggedParent = GetLoginParent();
            }

            int prosklisiId = Common.GetOpenProsklisiID();
            if (prosklisiId == 0)
            {
                return RedirectToAction("Index", "Parents");
            }
            int parentsId = Common.GetParentIdFromUserId(loggedParent.USER_ID);
            if (parentsId == 0)
            {
                string msg = "Πρέπει πρώτα να καταχωρηθούν τα στοιχεία γονέων στην 1η επιλογή.";
                return RedirectToAction("Index", "Parents", new { notify = msg });
            }
            ViewBag.ProsklisiData = Common.GetOpenProsklisi();

            var data = (from d in db.STATEMENTS where d.PROSKLISI_ID == prosklisiId && d.PARENTS_ID == parentsId select d).FirstOrDefault();
            if (data == null)
            {
                StatementViewModel model = new StatementViewModel()
                {
                    PROSKLISI_ID = prosklisiId,
                    PARENTS_ID = parentsId
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("StatementEdit", "Parents");
            }
        }

        [HttpPost]
        public ActionResult StatementCreate(StatementViewModel data)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("TaxisNetLogin", "USER_PARENTS");
            }
            else
            {
                loggedParent = GetLoginParent();
            }
            int parentsId = Common.GetParentIdFromUserId(loggedParent.USER_ID);
            int prosklisiId = Common.GetOpenProsklisiID();

            data.INCOME_CATEGORY = Common.ComputeIncomeCategory(data.INCOME_FAMILY);
            string ErrorMsg = Common.VerifyIncomeCategory(data.INCOME_CATEGORY);
            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω επικύρωσης δεδομένων. " + ErrorMsg);
                return View(data);
            }
            if (Kerberos.StatementExists(loggedParent.USER_ID))
            {
                this.ShowMessage(MessageType.Warning, "Υπάρχει ήδη δήλωση στοιχείων αυτού του γονέα. Η αποθήκευση ακυρώθηκε.");
                return View(data);
            }

            if (data != null && ModelState.IsValid)
            {
                statementService.CreateRecord(data, parentsId, prosklisiId);

                string msg = "Η αποθήκευση ολοκληρώθηκε με επιτυχία.";
                return RedirectToAction("StatementEdit", "Parents", new { notify = msg });
            }
            this.ShowMessage(MessageType.Error, "Η αποθήκευση απέτυχε λόγω σφαλμάτων καταχώρησης.");
            return View(data);
        }

        public ActionResult StatementEdit(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("TaxisNetLogin", "USER_PARENTS");
            }
            else
            {
                loggedParent = GetLoginParent();
            }

            int parentsId = Common.GetParentIdFromUserId(loggedParent.USER_ID);
            if (parentsId == 0)
            {
                string msg = "Πρέπει πρώτα να καταχωρηθούν τα στοιχεία γονέων στην 1η επιλογή.";
                return RedirectToAction("Index", "Parents", new { notify = msg });
            }
            int prosklisiId = Common.GetOpenProsklisiID();
            if (prosklisiId == 0)
            {
                string Msg = "Δεν βρέθηκε ανοικτή Πρόσκληση. Όλες οι ενέργειες δημιουργίας και επεξεργασίας δεδομένων είναι απενεργοποιημένες.";
                return RedirectToAction("Index", "Parents", new { notify = Msg });
            }
            if (notify != null) this.ShowMessage(MessageType.Success, notify);

            StatementViewModel data = statementService.GetRecord(parentsId, prosklisiId);
            if (data == null)
            {
                return RedirectToAction("StatementCreate", "Parents");
            }
            ViewBag.ProsklisiData = Common.GetOpenProsklisi();
            SetViewBagParentNames();
            return View(data);
        }

        [HttpPost]
        public ActionResult StatementEdit(StatementViewModel data)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("TaxisNetLogin", "USER_PARENTS");
            }
            else
            {
                loggedParent = GetLoginParent();
            }
            int parentsId = Common.GetParentIdFromUserId(loggedParent.USER_ID);
            int prosklisiId = Common.GetOpenProsklisiID();
            int statementId = 0;

            SetViewBagParentNames();

            var statement = (from d in db.STATEMENTS where d.PROSKLISI_ID == prosklisiId && d.PARENTS_ID == parentsId select d).FirstOrDefault();
            if (statement == null)
            {
                string msg = "Προέκυψε σφάλμα ανάκτησης δήλωσης στοιχείων.";
                return RedirectToAction("ErrorData", "Parents", new { notify = msg });
            }
            statementId = statement.STATEMENT_ID;
            
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
                return RedirectToAction("TaxisNetLogin", "USER_PARENTS");
            }
            else
            {
                loggedParent = GetLoginParent();
            }
            int parentId = loggedParentData.PARENTS_ID;
            int prosklisiId = Common.GetOpenProsklisiID();

            if (!AitisisExist())
            {
                string message = "Πρέπει να καταχωρήσετε μια αίτηση για εκτύπωση της Δήλωσης";
                return RedirectToAction("Index", "Parents", new { notify = message });
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

        public bool AitisisExist(int parentId, int proskilisiId)
        {
            var dataCount = (from d in db.AITISIS where d.PROSKLISI_ID == proskilisiId && d.PARENTS_ID == parentId select d).Count();
            if (dataCount == 0)
                return false;

            return true;
        }

        #endregion


        #region ΑΙΤΗΣΕΙΣ ΕΓΓΡΑΦΗΣ

        public ActionResult AitisisData(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("TaxisNetLogin", "USER_PARENTS");
            }
            else
            {
                loggedParent = GetLoginParent();
            }
            int prosklisiId = Common.GetOpenProsklisiID();
            if (prosklisiId == 0)
            {
                string Msg = "Δεν βρέθηκε ανοικτή Πρόσκληση. Όλες οι ενέργειες δημιουργίας και επεξεργασίας δεδομένων είναι απενεργοποιημένες.";
                return RedirectToAction("Index", "Parents", new { notify = Msg });
            }

            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            loggedParent = GetLoginParent();
            int parentsId = Common.GetParentIdFromUserId(loggedParent.USER_ID);
            if (!ChildrenExist(parentsId))
            {
                string msg = "Δεν βρέθηκαν παιδιά! Πρέπει πρώτα να καταχωρήσετε τα στοιχεία των παιδιών στην 2η επιλογή του μενού";
                return RedirectToAction("Index", "Parents", new { notify = msg });
            }

            if (!Common.ParentStatementExists(parentsId, prosklisiId))
            {
                string msg = "Για να καταχωρήσετε αίτηση πρέπει πρώτα να υποβάλετε δήλωση στοιχείων γονέων.";
                return RedirectToAction("Index", "Parents", new { notify = msg });
            }

            PopulateParentChildren(parentsId);
            PopulateStations();
            return View();
        }

        public ActionResult Aitisi_Read([DataSourceRequest] DataSourceRequest request)
        {
            int prosklisiId = Common.GetOpenProsklisiID();
            int parentsId = Common.GetParentIdFromUserId(GetLoginParent().USER_ID);

            IEnumerable<AitisiViewModel> data = aitisiService.Read(prosklisiId, parentsId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Aitisi_Create([DataSourceRequest] DataSourceRequest request, AitisiViewModel data)
        {
            var prosklisiId = Common.GetOpenProsklisiID();
            int parentsId = Common.GetParentIdFromUserId(GetLoginParent().USER_ID);
            bool isnew = true;

            AitisiViewModel newdata = new AitisiViewModel();

            if (!Kerberos.ValidateSameStations(parentsId, (int)data.STATION_ID, isnew))
            {
                ModelState.AddModelError("", "Αιτήσεις μπορούν να γίνουν μόνο στον ίδιο σταθμό. Η καταχώρηση ακυρώθηκε.");
            }
            if (Kerberos.IsDuplicateAitisi(parentsId, (int)data.CHILD_ID))
            {
                ModelState.AddModelError("", "Υπάρχει ήδη αίτηση για το παιδί αυτό. Η καταχώρηση ακυρώθηκε.");
            }
            if (data != null && ModelState.IsValid)
            {
                aitisiService.Create(data, prosklisiId, parentsId);
                newdata = aitisiService.Refresh(data.AITISI_ID);

                // Aitisi, Statement and Uploads must refer to the same station
                UpdateStatementStation(prosklisiId, parentsId, (int)data.STATION_ID);
                UpdateUploadsStation(prosklisiId, parentsId, (int)data.STATION_ID);

                if (!Common.VerifyUploadIntegrity(prosklisiId, loggedParent))
                {
                    string msg = "Ο σταθμός της αίτησης άλλαξε και δεν συμφωνεί με το σταθμό για τον οποίο έχουν ανέβει τα αρχεία. ";
                    msg += "Διαγράψτε τα ανεβασμένα αρχεία και μεταφορτώστε τα πάλι, αλλιώς δεν θα μπορούν να βρεθούν.";
                    ModelState.AddModelError("", msg);
                }
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Aitisi_Update([DataSourceRequest] DataSourceRequest request, AitisiViewModel data)
        {
            loggedParent = GetLoginParent();
            int parentsId = Common.GetParentIdFromUserId(loggedParent.USER_ID);
            var prosklisiId = Common.GetOpenProsklisiID();

            AitisiViewModel newdata = new AitisiViewModel();

            if (!Kerberos.ValidateSameStations(parentsId, (int)data.STATION_ID))
            {
                ModelState.AddModelError("", "Αιτήσεις μπορούν να γίνουν μόνο στον ίδιο σταθμό. Η καταχώρηση ακυρώθηκε.");
            }
            if (data != null && ModelState.IsValid)
            {
                aitisiService.Update(data, prosklisiId, parentsId);
                newdata = aitisiService.Refresh(data.AITISI_ID);

                // Aitisi, Statement and uploads must refer to the same station
                UpdateStatementStation(prosklisiId, parentsId, (int)data.STATION_ID);
                UpdateUploadsStation(prosklisiId, parentsId, (int)data.STATION_ID);

                if (!Common.VerifyUploadIntegrity(prosklisiId, loggedParent))
                {
                    string msg = "Ο σταθμός της αίτησης άλλαξε και δεν συμφωνεί με το σταθμό για τον οποίο έχουν ανέβει τα αρχεία.<br> ";
                    msg += "Διαγράψτε τα ανεβασμένα αρχεία και μεταφορτώστε τα πάλι, αλλιώς δεν θα μπορούν να βρεθούν.";
                    ModelState.AddModelError("", msg);
                }
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Aitisi_Destroy([DataSourceRequest] DataSourceRequest request, AitisiViewModel data)
        {
            if (Kerberos.CanDeleteAitisi(data.AITISI_ID))
            {
                aitisiService.Destroy(data);
            }
            else
            {
                string exception = "Δεν μπορεί να διαγραφεί η αίτηση διότι έχει συνημμένα αρχεία.<br>";
                exception += "Διαγράψτε πρώτα τα συνημμένα και ύστερα την αίτηση.";
                ModelState.AddModelError("", exception);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        // --------------------------
        // Αντικαθιστά τη συνηθισμένη action Destroy διότι όταν χρειάζεται
        // error message η ModelState.AddModelError έχει το σύμπτωμα να
        // καλεί ξανά την Destroy μετά από μια Create/Update Action!
        // Χρησιμοποείται σε συνδυασμό με την jQuery deleteRow() στον client.
        // Ημερομηνία : 26/03/2020
        // --------------------------
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Aitisi_Delete(int aitisiId = 0)
        {
            string msg = aitisiService.Delete(aitisiId);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        // Not used - 02/05/2020
        public void UpdateAitisiProtocol(int aitisisId)
        {
            AITISIS entity = db.AITISIS.Find(aitisisId);
            entity.AITISI_PROTOCOL = Common.GenerateProtocol();
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        // ensures aitiseis and statement refer to the same station.
        public void UpdateStatementStation(int prosklisiID, int parentsID, int stationID)
        {
            var data = (from d in db.STATEMENTS
                        where d.PROSKLISI_ID == prosklisiID && d.PARENTS_ID == parentsID
                        select d).FirstOrDefault();
            if (data == null)
                return;

            if (data.STATION_ID != stationID)
            {
                STATEMENTS entity = db.STATEMENTS.Find(data.STATEMENT_ID);

                entity.STATION_ID = stationID;
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
            return;
        }

        public void UpdateUploadsStation(int prosklisiID, int parentsID, int stationID)
        {
            var data = (from d in db.UPLOADS
                        where d.PROSKLISI_ID == prosklisiID && d.PARENT_ID == parentsID
                        select d).ToList();
            if (data.Count == 0)
                return;

            foreach (var item in data)
            {
                UPLOADS entity = db.UPLOADS.Find(item.UPLOAD_ID);

                entity.STATION_ID = stationID;
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
            return;
        }

        public ActionResult AitisiPrint(int aitisiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("TaxisNetLogin", "USER_PARENTS");
            }
            else
            {
                loggedParent = GetLoginParent();
            }
            AitisiParameters parameters = new AitisiParameters();
            parameters.AITISI_ID = aitisiId;
            return View(parameters);
        }

        #endregion


        #region ΜΕΤΑΦΟΡΤΩΣΗ ΠΙΣΤΟΠΟΙΗΤΙΚΩΝ

        public ActionResult UploadData(string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("TaxisNetLogin", "USER_PARENTS");
            }
            else
            {
                loggedParent = GetLoginParent();
            }
            int prosklisiId = Common.GetOpenProsklisiID();
            if (prosklisiId == 0)
            {
                string Msg = "Δεν βρέθηκε ανοικτή Πρόσκληση. Όλες οι ενέργειες δημιουργίας και επεξεργασίας δεδομένων είναι απενεργοποιημένες.";
                return RedirectToAction("Index", "Parents", new { notify = Msg });
            }

            if (notify != null)
            {
                this.ShowMessage(MessageType.Warning, notify);
            }
            if (!AitisisExist())
            {
                string msg = "Για να γίνει μεταφόρτωση πρέπει πρώτα να καταχωρήσετε αίτηση.";
                return RedirectToAction("Index", "Parents", new { notify = msg });
            }

            if (!Common.VerifyUploadIntegrity(prosklisiId, loggedParent))
            {
                notify = "Ο σταθμός της αίτησης δεν συμφωνεί με το σταθμό για τον οποίο έχουν ανέβει τα αρχεία. ";
                notify += "Διαγράψτε τα ανεβασμένα αρχεία και μεταφορτώστε τα πάλι, αλλιώς δεν θα μπορούν να βρεθούν.";
                this.ShowMessage(MessageType.Error, notify);
            }
            PopulateSchoolYears();
            PopulateAitisis();

            return View();
        }

        #region MASTER GRID CRUD FUNCTIONS

        public ActionResult Upload_Read([DataSourceRequest] DataSourceRequest request)
        {
            int prosklisiId = Common.GetOpenProsklisiID();
            int parentsId = Common.GetParentIdFromUserId(GetLoginParent().USER_ID);

            IEnumerable<UploadsViewModel> data = uploadService.Read(prosklisiId, parentsId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Upload_Create([DataSourceRequest] DataSourceRequest request, UploadsViewModel data)
        {
            loggedParent = GetLoginParent();
            int prosklisiId = Common.GetOpenProsklisiID();

            var newdata = new UploadsViewModel();

            if (data != null && ModelState.IsValid)
            {
                uploadService.Create(data, prosklisiId, loggedParent);
                newdata = uploadService.Refresh(data.UPLOAD_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Upload_Update([DataSourceRequest] DataSourceRequest request, UploadsViewModel data)
        {
            loggedParent = GetLoginParent();
            int prosklisiId = Common.GetOpenProsklisiID();

            var newdata = new UploadsViewModel();

            if (data != null && ModelState.IsValid)
            {
                uploadService.Update(data, prosklisiId, loggedParent);
                newdata = uploadService.Refresh(data.UPLOAD_ID);
            }
            return Json(new[] { newdata }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Upload_Destroy([DataSourceRequest] DataSourceRequest request, UploadsViewModel data)
        {
            if (data != null)
            {
                if (Kerberos.CanDeleteUpload(data.UPLOAD_ID))
                {
                    uploadService.Destroy(data);
                }
                else
                {
                    ModelState.AddModelError("", "Για να γίνει η διαγραφή πρέπει πρώτα να διαγραφούν τα σχετικά μεταφορτωμένα αρχεία.");
                }
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        // --------------------------
        // Αντικαθιστά τη συνηθισμένη action Destroy διότι όταν χρειάζεται
        // error message η ModelState.AddModelError έχει το σύμπτωμα να
        // καλεί ξανά την Destroy μετά από μια Create/Update Action!
        // Χρησιμοποείται σε συνδυασμό με την jQuery deleteRow() στον client.
        // Ημερομηνία : 26/03/2020
        // --------------------------
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Upload_Delete(int uploadId = 0)
        {
            string msg = uploadService.Delete(uploadId);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region CHILD GRID (UPLOADED FILEDETAILS)

        public ActionResult UploadFiles_Read([DataSourceRequest] DataSourceRequest request, int uploadId = 0)
        {
            IEnumerable<UploadsFilesViewModel> data = uploadService.ReadFiles(uploadId);

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UploadFiles_Destroy([DataSourceRequest] DataSourceRequest request, UploadsFilesViewModel data)
        {
            if (data != null)
            {
                // First delete the physical file and then the info record. Important!
                DeleteUploadedFile(data.ID);

                uploadService.DestroyFile(data);
            }
            return Json(new[] { data }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region UPLOAD FORM WITH SAVE-REMOVE ACTIONS

        public ActionResult UploadForm(int uploadId, string notify = null)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("TaxisNetLogin", "USER_PARENTS");
            }
            else
            {
                loggedParent = GetLoginParent();
                if (loggedParent == null)
                    return RedirectToAction("Error", "Parents", new { notify = "Δεν βρέθηκε εξουσιοδοτημένος χρήστης για το αίτημα." });
            }
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);
            if (!(uploadId > 0))
            {
                string msg = "Άκυρος κωδικός μεταφόρτωσης. Πρέπει πρώτα να αποθηκεύσετε την εγγραφή μεταφόρτωσης.";
                return RedirectToAction("ErrorData", "Parents", new { notify = msg });
            }
            ViewData["uploadId"] = uploadId;

            return View();
        }

        public ActionResult Upload(IEnumerable<HttpPostedFileBase> files, int uploadId = 0)
        {
            string folder = "";
            string uploadPath = UPLOAD_PATH;
            string subfolder = "";

            List<UploadsFilesViewModel> fileDetails = new List<UploadsFilesViewModel>();

            // returns tuple with Item1=station_id, Item2=prosklisi_id, Item3=aitisi_id
            var upload_info = Common.GetUploadInfo(uploadId);
            int schoolyearId = (int)db.PROSKLISIS.Find(upload_info.Item2).SCHOOL_YEAR;

            folder = Common.GetUserStationFromStationId(upload_info.Item1);
            subfolder = Common.GetSchoolYearText(schoolyearId);

            if (!String.IsNullOrEmpty(folder) && !String.IsNullOrEmpty(subfolder))
                uploadPath += folder + "/" + subfolder + "/";

            try
            {
                bool exists = System.IO.Directory.Exists(Server.MapPath(uploadPath));
                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(uploadPath));

                if (files != null)
                {
                    foreach (var file in files)
                    {
                        // Some browsers send file names with full path.
                        // We are only interested in the file name.
                        if (file.ContentLength > 0)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var fileExtension = Path.GetExtension(fileName);
                            if (!Kerberos.ValidFileExtension(fileExtension))
                            {
                                string msg = "Επιτρέπονται μόνο αρχεία τύπου PDF, JPG, JPEG. Δοκιμάστε πάλι.";
                                return Content(msg);
                            }
                            UPLOADS_FILES fileDetail = new UPLOADS_FILES()
                            {
                                FILENAME = fileName.Length > 120 ? fileName.Substring(0, 120) : fileName,
                                EXTENSION = fileExtension,
                                STATION_USER = folder,
                                SCHOOLYEAR_TEXT = subfolder,
                                UPLOAD_ID = uploadId,
                                ID = Guid.NewGuid()
                            };
                            db.UPLOADS_FILES.Add(fileDetail);
                            db.SaveChanges();

                            var physicalPath = Path.Combine(Server.MapPath(uploadPath), fileDetail.ID + fileDetail.EXTENSION);
                            file.SaveAs(physicalPath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = "Παρουσιάστηκε σφάλμα στη μεταφόρτωση:<br/>" + ex.Message;
                return Content(msg);
            }
            // Return an empty string to signify success
            return Content("");
        }


        // NOT USED - 26.10.2022 (replaced by Kerberos function)
        public bool ValidFileExtension(string extension)
        {
            string[] extensions = { ".EXE", ".COM", "BAT", ".MSI", ".BIN", ".CMD", ".JSE", ".REG", ".VBS", ".VBE", ".WS", ".WSF" };

            List<string> forbidden_extensions = new List<string>(extensions);

            if (forbidden_extensions.Contains(extension.ToUpper()))
                return false;
            return true;
        }

        public ActionResult Remove(string[] fileNames, int uploadId)
        {
            // The parameter of the Remove action must be called "fileNames"
            string folder;
            string uploadPath = UPLOAD_PATH;
            string subfolder;

            // returns tuple with Item1=station_id, Item2=prosklisi_id, Item3=aitisi_id
            var upload_info = Common.GetUploadInfo(uploadId);
            int schoolyearId = (int)db.PROSKLISIS.Find(upload_info.Item2).SCHOOL_YEAR;

            folder = Common.GetUserStationFromStationId(upload_info.Item1);
            subfolder = Common.GetSchoolYearText(schoolyearId);

            if (!string.IsNullOrEmpty(folder) && !string.IsNullOrEmpty(subfolder))
                uploadPath += folder + "/" + subfolder + "/";

            if (fileNames != null)
            {
                foreach (var fullName in fileNames)
                {
                    var fileName = Path.GetFileName(fullName);
                    var extension = Path.GetExtension(fileName);

                    Guid file_guid = Common.GetFileGuidFromName(fileName, uploadId);

                    string fileToDelete = file_guid + extension;
                    var physicalPath = Path.Combine(Server.MapPath(uploadPath), fileToDelete);

                    if (System.IO.File.Exists(physicalPath))
                    {
                        System.IO.File.Delete(physicalPath);
                        DeleteUploadFileRecord(file_guid);
                    }
                }
            }
            // Return an empty string to signify success
            return Content("");
        }

        public FileResult Download(Guid file_id)
        {
            String p = "";
            String f = "";
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

        public ActionResult DeleteUploadFileRecord(Guid file_guid)
        {
            UPLOADS_FILES entity = db.UPLOADS_FILES.Find(file_guid);
            if (entity != null)
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.UPLOADS_FILES.Remove(entity);
                db.SaveChanges();
            }
            return Content("");
        }

        public void DeleteUploadedFile(Guid file_guid)
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

                if (!string.IsNullOrEmpty(folder) && !string.IsNullOrEmpty(subfolder))
                    uploadPath += folder + "/" + subfolder + "/";

                string fileToDelete = file_guid + extension;
                var physicalPath = Path.Combine(Server.MapPath(uploadPath), fileToDelete);
                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }
            }
            //return Content("");
        }

        #endregion

        #endregion

        
        #region ΑΞΙΟΛΟΓΗΣΗ ΑΙΤΗΣΕΩΝ

        public ActionResult AitisisResults()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("TaxisNetLogin", "USER_PARENTS");
            }
            else
            {
                loggedParent = GetLoginParent();
            }

            bool viewAllowed = Common.GetProsklisiUserView();
            if (viewAllowed == false)
            {
                this.ShowMessage(MessageType.Warning, "Η προβολή αποτελεσμάτων αξιολόγησης είναι προσωρινά κλειδωμένη.");
            }

            PopulateAgeClasses();
            return View();
        }

        public ActionResult AitisisResults_Read([DataSourceRequest] DataSourceRequest request)
        {
            List<AitiseisListViewModel> data = GetAitisisResultsFromDB();

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public List<AitiseisListViewModel> GetAitisisResultsFromDB()
        {
            List<AitiseisListViewModel> data = new List<AitiseisListViewModel>();

            bool viewAllowed = Common.GetProsklisiUserView();
            int parentsId = Common.GetParentIdFromUserId(GetLoginParent().USER_ID);

            if (viewAllowed == true)
            {
                data = (from d in db.sqlAITISEIS_LIST
                        where d.PARENTS_ID == parentsId
                        orderby d.AITISI_PROTOCOL descending
                        select new AitiseisListViewModel
                        {
                            AITISI_ID = d.AITISI_ID,
                            AITISI_PROTOCOL = d.AITISI_PROTOCOL,
                            STATION_NAME = d.STATION_NAME,
                            CHILD_FULLNAME = d.CHILD_FULLNAME,
                            AGE_CATEGORY = d.AGE_CATEGORY,
                            MORIA_TOTAL = d.MORIA_TOTAL
                        }).ToList();
            }
            return data;
        }

        public ActionResult AitisiMoria(int aitisiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
                return RedirectToAction("Login", "USER_PARENTS");
            }
            else
            {
                loggedParent = GetLoginParent();
            }

            int schoolYearId = (int)Common.GetActiveProsklisi().SCHOOL_YEAR;
            ViewData["prosklisiProtocol"] = Common.GetActiveProsklisi().PROTOCOL;
            ViewData["schoolYearText"] = Common.GetSchoolYearText(schoolYearId);

            ChildInfoViewModel child_info = aitisisMoriaService.GetChildDetail(aitisiId);
            AitisiCheckViewModel data = aitisisMoriaService.GetAitisi(aitisiId);

            ViewBag.childInfo = child_info;
            return View(data);
        }

        public ActionResult AitisiMoriaPrint(int aitisiId)
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                return RedirectToAction("Login", "USER_PARENTS");
            }
            else
            {
                AitisiParameters parameters = new AitisiParameters();
                parameters.AITISI_ID = aitisiId;
                return View(parameters);
            }
        }

        #endregion


        #region EXISTORS, SETTERS

        public bool AitisisExist()
        {
            loggedParent = GetLoginParent();
            int prosklisiId = Common.GetOpenProsklisiID();

            int parentId = Common.GetParentIdFromUserId(loggedParent.USER_ID);
            if (parentId > 0)
            {
                var aitiseis = (from d in db.AITISIS where d.PROSKLISI_ID == prosklisiId && d.PARENTS_ID == parentId select d).ToList();
                if (aitiseis.Count > 0)
                    return true;
            }
            return false;
        }

        public bool ParentsExist(string afm)
        {
            var data = (from d in db.PARENTS where d.FATHER_AFM == afm || d.MOTHER_AFM == afm select d).ToList();
            if (data.Count == 0)
                return false;
            return true;
        }

        public bool ChildrenExist(int parentsId)
        {
            var data = (from d in db.sqlCHILD_SELECTOR where d.PARENTS_ID == parentsId select d).ToList();
            if (data.Count == 0)
                return false;
            return true;
        }

        public void SetViewBagParentNames()
        {
            loggedParent = db.USER_PARENTS.Where(m => m.USER_AFM == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
            if (loggedParent == null)
            {
                ViewBag.FatherName = "** ΔΕΝ ΒΡΕΘΗΚΕ **";
                ViewBag.MotherName = "** ΔΕΝ ΒΡΕΘΗΚΕ **";
            }
            loggedParentData = (from d in db.PARENTS where d.PARENT_USERID == loggedParent.USER_ID select d).FirstOrDefault();
            if (loggedParentData != null)
            {
                ViewBag.FatherName = loggedParentData.FATHER_LASTNAME + " " + loggedParentData.FATHER_FIRSTNAME;
                ViewBag.MotherName = loggedParentData.MOTHER_LASTNAME + " " + loggedParentData.MOTHER_FIRSTNAME;
            }
        }

        #endregion
    }
}
