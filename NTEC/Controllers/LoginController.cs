using NTEC.Classes.Security;
using NTEC.Models.User;
using NTEC.ViewModels;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using Newtonsoft.Json.Linq;
using NTEC.Classes.Helpers;
using NTEC.Models;
using NTEC.Models.Entities;
using NTEC.Models.UnitOfWork;

namespace NTEC.Controllers
{
    [AllowAnonymous]
    public class LoginController : BaseController
    {
        //
        // GET: /Login/

        LoginRepository _objRepository = new LoginRepository();
        IUnitOfWork _uow;

        public LoginController()
        {
            _uow = new UnitOfWork();
        }
        public ActionResult Login()
        {
            ViewBag.Title = "NTEC";

            return View();
        }


        [HttpPost]
        public ActionResult LoginUser(UserLoginVM pLogin)
        {
            string jsonResult = _objRepository.LoginUser(pLogin);
            dynamic data = JObject.Parse(jsonResult);
          
            if (data.success == "True")
            {
                _uow.AutitTrails.Add(new AuditTrial()
                {
                    UserId = SessionManager.CurrentUser.UserId,
                    EventTypeId = 8,
                    EventTime = DateTime.Now,
                    EventDetails = "User Machine IP is " + Request.UserHostAddress,
                    EventDetailsAr = "رقم جهاز المستخدم هو  " + Request.UserHostAddress
                });
                _uow.Save();
                return Json(jsonResult);
            }
            _uow.AutitTrails.Add(new AuditTrial()
            {

                EventTypeId = 8,
                EventTime = DateTime.Now,
                EventDetails = "this Machine IP is " + Request.UserHostAddress + " " + "tried to log in with invalid username or password",
                EventDetailsAr = "جهاز هذا المستخدم   " + Request.UserHostAddress + " " + "حاول الدخول علي البرنامج باسم مستخدم او كلمة مرور خاطئة"
            });
            _uow.Save();
            return Json(jsonResult);
        }

        [HttpGet]
        public JsonResult SetLanguage(int landId)
        {
            UserLanguage obj = new UserLanguage();

            var urlReferrer = this.Request.UrlReferrer;

                string jsonResult = obj.AppLanguage(landId, urlReferrer.AbsolutePath);
            Session["langId"] = landId;
            return Json(jsonResult, JsonRequestBehavior.AllowGet);

        }



        [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public ActionResult SetCulture()
        {
            // Validate input
            int langId = Convert.ToInt32(SessionManager.LanguageId);
            string culture = "";
            culture = langId == 1 ? "ar" : "en-us";
            culture = CultureHelper.GetImplementedCulture(culture);

            // Save culture in a cookie
            HttpCookie cookie = Request.Cookies["_culture"];
            if (cookie != null)
                cookie.Value = culture;   // update cookie value
            else
            {

                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);

            return RedirectToAction("Login");

            //return View();

        }
        [HttpGet]
        public ActionResult SetInsideCulture(int landId)
        {
            //Validate input

            UserLanguage obj = new UserLanguage();
            var urlReferrer = this.Request.UrlReferrer;
            if (urlReferrer != null)
            {
                string jsonResult = obj.AppLanguage(landId, urlReferrer.AbsolutePath);
                dynamic data = JObject.Parse(jsonResult);

                if (data.success == "True")
                {

                    int langId = Convert.ToInt32(SessionManager.LanguageId);
                    string culture = "";
                    culture = langId == 1 ? "ar" : "en-us";
                    culture = CultureHelper.GetImplementedCulture(culture);

                    // Save culture in a cookie
                    HttpCookie cookie = Request.Cookies["_culture"];
                    if (cookie != null)
                        cookie.Value = culture;   // update cookie value
                    else
                    {

                        cookie = new HttpCookie("_culture")
                        {
                            Value = culture,
                            Expires = DateTime.Now.AddYears(1)
                        };
                    }
                    Response.Cookies.Add(cookie);
                    return Json(new { success = true }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            //return View();

        }
        public RedirectToRouteResult LogOff()
        {
            FormsAuthentication.SignOut();
            if (SessionManager.CurrentUser != null && SessionManager.CurrentUser.UserId != 0)
            {
                _uow.AutitTrails.Add(new AuditTrial()
                {
                    UserId = SessionManager.CurrentUser.UserId,
                    EventTypeId = 9,
                    EventTime = DateTime.Now,
                    EventDetails = "Logged out",
                    EventDetailsAr = "قام بتسجيل خروج"
                });
            }
            else
            {
                _uow.AutitTrails.Add(new AuditTrial()
                {

                    EventTypeId = 9,
                    EventTime = DateTime.Now,
                    EventDetails = "User Cleared His Brwoser Cookie before Logging out",
                    EventDetailsAr = "المستخدم قام بمسح البيانات ملفات الربط قبل تسجيل خروجه "
                });
            }

            _uow.Save();


            return RedirectToAction("Login");
        }

        public void TrakCloisngApp()
        {
            if (SessionManager.CurrentUser != null && SessionManager.CurrentUser.UserId != 0)
            {
                _uow.AutitTrails.Add(new AuditTrial()
                {
                    UserId = SessionManager.CurrentUser.UserId,
                    EventTypeId = 9,
                    EventTime = DateTime.Now,
                    EventDetails = "Closing Web Browser 'Not Logging out'",
                    EventDetailsAr = "قام باغلاق البرنامج وليس تسجيل خروج "
                });
            }
            else
            {
                _uow.AutitTrails.Add(new AuditTrial()
                {
                   
                    EventTypeId = 9,
                    EventTime = DateTime.Now,
                    EventDetails = "User Cleared His Brwoser Cookie before Closing Web Browser 'Not Logging out'",
                    EventDetailsAr = "المستخدم قام بمسح البيانات ملفات الربط قبل تسجيل خروجه "
                });
            }

            _uow.Save();

        }
    }
}
