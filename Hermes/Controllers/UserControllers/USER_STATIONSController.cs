using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using Kendo.Mvc.Extensions;
using Hermes.Models;
using Hermes.BPM;
using Hermes.DAL;
using Hermes.DAL.Security;
using Hermes.Filters;


namespace Hermes.Controllers.UserControllers
{
    [ErrorHandlerFilter]
    public class USER_StationSController : Controller
    {
        private USER_STATIONS loggedStation;
        private readonly HermesDBEntities db;

        public USER_StationSController(HermesDBEntities entities)
        {
            db = entities;
        }


        public ActionResult Login()
        {
            bool val1 = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!val1)
            {
                ViewBag.loggedUser = "(χωρίς σύνδεση)";
            }
            else
            {
                loggedStation = db.USER_STATIONS.Where(u => u.USERNAME == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();
                if (loggedStation != null)
                {
                    ViewBag.loggedUser = GetLoginStation();
                    return RedirectToAction("Index", "Station");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "USERNAME,PASSWORD")]  UserStationViewModel model)
        {
            var user = db.USER_STATIONS.Where(u => u.USERNAME == model.USERNAME && u.PASSWORD == model.PASSWORD).FirstOrDefault();

            if (user != null)
            {
                WriteUserCookie(model);
                SetLoginStatus(user, true);
                return RedirectToAction("Index", "Station");
            }
            ModelState.AddModelError("", "Το όνομα χρήστη ή/και ο κωδ.πρόσβασης δεν είναι σωστά");
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult LogOut([Bind(Include = "ISACTIVE")] USER_STATIONS userStation)
        {
            var user = db.USER_STATIONS.Where(u => u.USERNAME == userStation.USERNAME && u.PASSWORD == userStation.PASSWORD).FirstOrDefault();

            FormsAuthentication.SignOut();
            SetLoginStatus(user, false);

            return RedirectToAction("Index", "Home");
        }

        public void WriteUserCookie(UserStationViewModel user)
        {
            StationPrincipalSerializeModel serializeModel = new StationPrincipalSerializeModel();
            serializeModel.UserId = user.USER_ID;
            serializeModel.Username = user.USERNAME;
            serializeModel.StationId = user.STATION_ID ?? 0;

            string userData = JsonConvert.SerializeObject(serializeModel);
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                1, user.USERNAME, DateTime.Now, DateTime.Now.AddMinutes(Kerberos.TICKET_TIMEOUT_MINUTES), false, userData);
            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(faCookie);
        }

        public void SetLoginStatus(USER_STATIONS user, bool value)
        {
            db.Entry(user).State = EntityState.Modified;
            user.ISACTIVE = value;
            db.SaveChanges();
        }

        public USER_STATIONS GetLoginStation()
        {
            loggedStation = db.USER_STATIONS.Where(u => u.USERNAME == System.Web.HttpContext.Current.User.Identity.Name).FirstOrDefault();

            int StationID = loggedStation.STATION_ID ?? 0;
            var _Station = (from s in db.sqlUSER_STATION
                            where s.STATION_ID == StationID
                            select new { s.ΕΠΩΝΥΜΙΑ }).FirstOrDefault();

            ViewBag.loggedUser = _Station.ΕΠΩΝΥΜΙΑ;
            return loggedStation;
        }

    }
}
