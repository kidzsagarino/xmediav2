using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.Login;
using BusinessRef.ModelObject.Login;
using BusinessRef.Interfaces.Generics;
using BusinessRef.Interfaces.Login;

namespace xmedia.Controllers
{
    public class LoginUserAccountController : Controller
    {
        // GET: LoginUserAccount
        public ActionResult Index()
        {
            return View("~/Views/LoginUserAccount/Login.cshtml");
        }

        [HttpPost]
        public JsonResult UserLoginAccount(LoginInfoUserModel Model)
        {
            IGetLoginReturnData data = new LoginExistingUserDataLogic(Model);
            return Json(data.GetLoginReturnData(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SignUp()
        {
            return View("~/Views/LoginUserAccount/SignUp.cshtml");
        }

        [HttpPost]
        public ActionResult SignUp(LoginNewUserAccountDataModel newUserAccountDataModel)
        {
            return null;
        }

        public ActionResult ForgotPassword()
        {
            return View("~/Views/LoginUserAccount/ForgotPassword.cshtml");
        }

        [HttpPost]
        public ActionResult ForgorPassword(LoginInfoUserModel loginInfo)
        {
            return null;
        }
    }
}