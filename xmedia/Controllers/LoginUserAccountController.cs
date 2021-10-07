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
            return View();
        }

        [HttpPost]
        public JsonResult UserLoginAccount(LoginInfoUserModel Model)
        {
            IGetLoginReturnData data = new LoginExistingUserDataLogic(Model);
            return Json(data.GetLoginReturnData(), JsonRequestBehavior.AllowGet);
        }
    }
}