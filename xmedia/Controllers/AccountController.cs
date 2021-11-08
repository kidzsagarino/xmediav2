using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.Account;
using BusinessRef.ModelObject.Account;
using BusinessRef.Interfaces.Generics;
using BusinessRef.Interfaces.Account;

namespace xmedia.Controllers
{
    public class AccountController : Controller
    {
        // GET: Default
        public ActionResult Login()
        {
            return View("~/Views/Account/Login.cshtml");
        }

        [HttpPost]
        public JsonResult Login(LoginInfoUserModel Model)
        {
            IGetLoginReturnData data = new LoginExistingUserDataLogic(Model);
            return Json(data.GetLoginReturnData(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SignUp()
        {
            return View("~/Views/Account/Signup.cshtml");
        }

        [HttpPost]
        public ActionResult SignUp(LoginNewUserAccountDataModel model)
        {
            string root = Server.MapPath("~/files/profileimage/");

            IGetLoginReturnData data = new LoginNewUserAccountDataLogic(model, root);
            return Json(data.GetLoginReturnData(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ForgotPassword()
        {
            return View("~/Views/Account/ForgotPassword.cshtml");
        }

        [HttpPost]
        public ActionResult ForgorPassword(LoginInfoUserModel loginInfo)
        {
            return null;
        }
    }
}