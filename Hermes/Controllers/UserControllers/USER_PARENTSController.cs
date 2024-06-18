using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Hermes.Models;
using Hermes.BPM;
using Hermes.DAL;
using Hermes.DAL.Security;
using Hermes.Filters;
using Hermes.Notification;
using Hermes.CAPTCHA;


namespace Hermes.Controllers.UserControllers
{
    [ErrorHandlerFilter]
    public class USER_PARENTSController : Controller
    {
        private USER_PARENTS loggedParent;
        private readonly HermesDBEntities db;

        public USER_PARENTSController(HermesDBEntities entities)
        {
            db = entities;
        }


        #region NOT USED AS LOGIN IS EXTERNAL FROM TAXISNET (08/05/2020)

        public ActionResult Login(string notify = null)
        {
            bool AppStatusOn = true;
            try
            {
                AppStatusOn = GetApplicationStatus();
                if (AppStatusOn == false)
                {
                    return RedirectToAction("AppStatusOff", "Home");
                }
            }
            catch
            {
                return RedirectToAction("ErrorConnect", "Home");
            }

            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
            }
            else
            {
                var data = (from d in db.USER_PARENTS where d.USER_AFM == System.Web.HttpContext.Current.User.Identity.Name select d).FirstOrDefault();
                if (data != null)
                {
                    loggedParent = data;
                    ViewBag.loggedUser = loggedParent.USERNAME;
                    return RedirectToAction("Index", "Parents");
                }
            }
            if (notify != null)
            {
                this.ShowMessage(MessageType.Info, notify);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "USERNAME, PASSWORD, CAPTCHATEXT")]  UserParentViewModel model)
        {
            var user = db.USER_PARENTS.Where(u => u.USERNAME == model.USERNAME && u.PASSWORD == model.PASSWORD).FirstOrDefault();

            if (user != null)
            {
                CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                serializeModel.UserId = model.USER_ID;
                serializeModel.Username = model.USERNAME;
                serializeModel.Afm = model.USER_AFM;

                string userData = JsonConvert.SerializeObject(serializeModel);
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                    1, user.USER_AFM, DateTime.Now, DateTime.Now.AddMinutes(Kerberos.TICKET_TIMEOUT_MINUTES), false, userData);
                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Cookies.Add(faCookie);

                return RedirectToAction("Index", "Parents");
            }

            ModelState.AddModelError("", "Το όνομα χρήστη ή/και ο κωδ.πρόσβασης δεν είναι σωστά");
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register([Bind(Include = "USERNAME, PASSWORD, ConfirmPassword, AFM, PARENT_TYPE")] NewUserParentViewModel UserParent)
        {
            if (ModelState.IsValid)
            {
                USER_PARENTS newUserParent = new USER_PARENTS()
                {
                    USERNAME = UserParent.USERNAME,
                    PASSWORD = UserParent.PASSWORD,
                    USER_AFM = UserParent.AFM.Trim(),
                    PARENT_TYPE = UserParent.PARENT_TYPE,
                    CREATEDATE = DateTime.Now
                };
                if (!Kerberos.existsUsername(UserParent.USERNAME))
                {
                    if (Kerberos.CheckAFM(UserParent.AFM))
                    {
                        if (AccountExists(UserParent.AFM))
                        {
                            string msg = "Υπάρχει ήδη λογαριασμός με αυτό το ΑΦΜ.";
                            return RedirectToAction("Manage", new {  notify = msg });
                        }
                        else
                        {
                            if (!VerifyUsernamePassword(UserParent.USERNAME, UserParent.PASSWORD))
                            {
                                this.ShowMessage(MessageType.Error, "Το όνομα χρήστη και ο κωδικός δεν πρέπει να περιέχουν κενούς χαρακτήρες!");
                                return View(UserParent);
                            }
                            db.USER_PARENTS.Add(newUserParent);
                            db.SaveChanges();
                            return RedirectToAction("Login", new { notify = "Η δημιουργία λογαριασμού ολοκληρώθηκε. Κάνετε χρήση των στοιχείων σας για είσοδο." });
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("AFM", "Το ΑΦΜ που δώσατε δεν είναι έγκυρο.");
                        return View(UserParent);
                    }
                }
                else
                {
                    ModelState.AddModelError("USERNAME", "Υπάρχει ήδη καταχωρημένος χρήστης με αυτό το Όνομα Χρήστη. Παρακαλώ διαλέξτε διαφορετικό Όνομα Χρήστη");
                    return View(UserParent);
                }
            }

            return View(UserParent);
        }

        public bool VerifyUsernamePassword(string username, string password)
        {
            if (Kerberos.CountSpaces(username) > 0 || Kerberos.CountSpaces(password) > 0) return false;
            else return true;
        }

        #endregion


        #region CAPTCHA HANDLERS NOT USED (08-05-2020)

        [AllowAnonymous]
        public ActionResult generateCaptcha()
        {
            System.Drawing.FontFamily family = new System.Drawing.FontFamily("Arial");
            CaptchaImage img = new CaptchaImage(180, 60, family);
            string text = img.CreateRandomText(6);   //+ " " + img.CreateRandomText(3);
            img.SetText(text);
            img.GenerateImage();
            img.Image.Save(Server.MapPath("~/CAPTCHA/") + this.Session.SessionID.ToString() + ".png", System.Drawing.Imaging.ImageFormat.Png);
            Session["captchaText"] = text;
            Session["filename"] = Server.MapPath("~/CAPTCHA/") + this.Session.SessionID.ToString() + ".png";
            return Json(Url.Content("~/CAPTCHA/") + this.Session.SessionID.ToString() + ".png?t=" + DateTime.Now.Ticks, JsonRequestBehavior.AllowGet);
        }

        public bool ValidateCaptcha(string strCaptcha)
        {
            string validText = Session["captchaText"].ToString();

            // delete the png file as no longer needed
            string completePath = Session["filename"].ToString();
            if ((System.IO.File.Exists(completePath))) System.IO.File.Delete(completePath);

            if (strCaptcha == validText) return true;

            return false;
        }

        public void CleanupCaptchaImages()
        {
            string[] files = System.IO.Directory.GetFiles(Server.MapPath("~/CAPTCHA/"), "*.png");

            foreach (string file in files)
            {
                if (System.IO.File.Exists(file))
                    System.IO.File.Delete(file);
            }
        }

        #endregion


        #region NEW USER-REGISTER USING TAXISnet

        // GET: RegisterUser <- accepts a random integer as Id of ΑΦΜ
        public ActionResult RegisterUser(int rnd_numberID = 0, string Afm = null)
        {
            ViewBag.loggedUser = "(χωρίς σύνδεση)";
            string userAFM = "";

            if (!String.IsNullOrEmpty(Afm))
                userAFM = Afm;
            else
                userAFM = GetTaxisnetAfm(rnd_numberID);

            if (String.IsNullOrEmpty(userAFM))
            {
                string msg = "Δεν βρέθηκαν στοιχεία εισόδου (ΑΦΜ) από το TAXISnet";
                return RedirectToAction("ErrorUser", "USER_PARENTS", new { notify = msg });
            }
            UserParentViewModel model = GetUserParentFromDB(userAFM);
            if (model == null)
            {
                UserParentViewModel newmodel = new UserParentViewModel()
                {
                    USER_AFM = userAFM,
                    USERNAME = userAFM,
                    PASSWORD = "XXXXXXXXXX",
                    CREATEDATE = DateTime.Now
                };
                return View(newmodel);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult RegisterUser(UserParentViewModel UserParent, int rnd_numberID = 0, string Afm = null)
        {
            string userAFM;

            if (!string.IsNullOrEmpty(Afm))
                userAFM = Afm;
            else
                userAFM = GetTaxisnetAfm(rnd_numberID);

            var user = GetUserParentFromDB(userAFM);
            if (user != null)
            {
                WriteUserCookie(user);
                DeleteTaxisRecord(rnd_numberID);

                return RedirectToAction("Index", "Parents");
            }

            // User does not exist, so create one
            if (ModelState.IsValid)
            {
                USER_PARENTS newUserParent = new USER_PARENTS()
                {
                    USER_AFM = userAFM,
                    USERNAME = userAFM,
                    PASSWORD = "XXXXXXXXXX",
                    PARENT_TYPE = UserParent.PARENT_TYPE,
                    CREATEDATE = DateTime.Now
                };
                db.USER_PARENTS.Add(newUserParent);
                db.SaveChanges();

                UserParentViewModel data = GetUserParentFromDB(userAFM);
                WriteUserCookie(data);
                DeleteTaxisRecord(rnd_numberID);

                return RedirectToAction("Index", "Parents");
            }
            UserParent.USER_AFM = userAFM;
            UserParent.USERNAME = userAFM;
            UserParent.PASSWORD = "XXXXXXXXXX";
            return View(UserParent);
        }

        public void WriteUserCookie(UserParentViewModel user)
        {
            if (user != null)
            {
                CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                serializeModel.UserId = user.USER_ID;
                serializeModel.Username = user.USERNAME;
                serializeModel.Afm = user.USER_AFM;

                string userData = JsonConvert.SerializeObject(serializeModel);
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, user.USER_AFM, DateTime.Now, DateTime.Now.AddMinutes(Kerberos.TICKET_TIMEOUT_MINUTES), false, userData);
                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                Response.Cookies.Add(faCookie);
            }
        }

        public void DeleteTaxisRecord(int rnd_numberID = 0)
        {
            var data = (from d in db.TAXISNET where d.RANDOM_NUMBER == rnd_numberID select d).FirstOrDefault();
            if (data == null)
                return;

            TAXISNET entity = db.TAXISNET.Find(data.TAXISNET_ID);
            if (entity != null) 
            {
                db.Entry(entity).State = EntityState.Deleted;
                db.TAXISNET.Remove(entity);
                db.SaveChanges();
            };
        }

        public UserParentViewModel GetUserParentFromDB(string Afm)
        {
            var data = (from d in db.USER_PARENTS
                        where d.USER_AFM == Afm
                        select new UserParentViewModel
                        {
                            USER_ID = d.USER_ID,
                            USER_AFM = d.USER_AFM,
                            USERNAME = d.USERNAME,
                            PASSWORD = d.PASSWORD,
                            CREATEDATE = d.CREATEDATE,
                            PARENT_TYPE = d.PARENT_TYPE
                        }).FirstOrDefault();
            return (data);
        }

        #endregion


        #region REDIRECTION TO EXTERNAL TAXISnet URL AND ERROR PAGES

        /// <summary>
        /// Redirect to TAXISnet Login Application. TODO
        /// </summary>
        /// <returns></returns>
        public ActionResult TaxisNetLogin()
        {
            // TEST: Link to Google maps
            //string address = "Laodikis 31";
            //string Area = "Glyfada";
            //string city = "Athens";
            //string zipCode = "16674";

            //var segment = string.Join(" ", address, Area, city, zipCode);
            //var escapedSegment = Uri.EscapeDataString(segment);

            //var baseFormat = "https://www.google.co.za/maps/search/{0}/";
            //var url = string.Format(baseFormat, escapedSegment);
            //return new RedirectResult(url);
            var data = (from d in db.APP_STATUS select d).FirstOrDefault();
            if (data.PARENT_ENABLE == false)
                return RedirectToAction("AppClosed", "USER_PARENTS");

            // ---- This is the actual Url and it works ------------
            string url = "http://auth.oaed.gr/Default.aspx?vns=true";
            return new RedirectResult(url);
        }

        public ActionResult Error(string notify = null)
        {
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            return View();
        }

        public ActionResult ErrorUser(string notify = null)
        {
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            return View();
        }

        public ActionResult AppClosed(string notify = null)
        {
            if (notify != null) this.ShowMessage(MessageType.Warning, notify);

            ViewData["message"] = "Η εφαρμογή είναι κλειστή για αιτήσεις εγγραφής διότι δεν υπάρχει τρέχουσα ανοικτή πρόσκληση.";

            return View();
        }

        #endregion


        #region USER-PARENT SELECTOR FOR TESTING

        public ActionResult UserParentsList()
        {
            string userTxt = "(χωρίς σύνδεση)";
            ViewBag.loggedUser = userTxt;

            List<sqlUserParentViewModel> data = GetUserParentListFromDB();

            return View(data);
        }

        public ActionResult UserParent_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = GetUserParentListFromDB();

            var result = new JsonResult();
            result.Data = data.ToDataSourceResult(request);
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return result;
        }

        public List<sqlUserParentViewModel> GetUserParentListFromDB()
        {
            var users = (from a in db.sqlUSER_PARENT_SELECT
                         orderby a.PARENT_FULLNAME
                         select new sqlUserParentViewModel
                         {
                             USER_ID = a.USER_ID,
                             USER_AFM = a.USER_AFM,
                             CREATEDATE = a.CREATEDATE,
                             PARENT_FULLNAME = a.PARENT_FULLNAME,
                             PARENT_TYPETEXT = a.PARENT_TYPETEXT
                         }).ToList();

            return users;
        }

        #endregion


        #region GETTERS NEW (08/05/2020)

        public string GetTaxisnetAfm(int rnd_numberID = 0)
        {
            string safm = "";
            var data = (from d in db.TAXISNET where d.RANDOM_NUMBER == rnd_numberID select d).FirstOrDefault();
            if (data != null)
            {
                safm = data.TAXISNET_AFM;
            }
            return safm;
        }

        public bool AccountExists(string safm)
        {
            var user = db.USER_PARENTS.Where(u => u.USER_AFM == safm).FirstOrDefault();

            if (user != null) return true;
            else return false;

        }

        public bool GetApplicationStatus()
        {
            var data = (from d in db.APP_STATUS select d).FirstOrDefault();
            bool status = data.STATUS_VALUE ?? false;
            return status;
        }

        public bool isApplicationLocal()
        {
            var data = (from d in db.APP_STATUS select d).FirstOrDefault();
            bool status = data.LOCAL_TEST ?? false;
            return status;
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

        [AllowAnonymous]
        public ActionResult LogOut([Bind(Include = "ISACTIVE")] USER_PARENTS userParent)
        {
            var user = db.USER_PARENTS.Where(u => u.USERNAME == userParent.USERNAME && u.PASSWORD == userParent.PASSWORD).FirstOrDefault();

            FormsAuthentication.SignOut();

            // maybe we need to add that the profile is empty
            // so when the home page is displayed, it shows "No Connection"
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}
