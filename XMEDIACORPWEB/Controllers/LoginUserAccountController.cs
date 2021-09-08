using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessModel.ObjectClass;
using BusinessModel.ObjectModel;
using BusinessModel.Interfaces;
using BusinessLogic;

namespace XMEDIACORPWEB.Controllers
{
    public class LoginUserAccountController : Controller
    {
        // GET: LoginExistingAccount
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult ExistingUserLoginAccount()
        {
            return View();
        }
       
    }
}