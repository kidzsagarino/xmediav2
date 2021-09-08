using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogic.ForgotPassword;
using BusinessModel.ObjectModel.ForgotPassword;
using BusinessModel.Interfaces;
using BusinessModel.Interfaces.ForgotPassword;

namespace XMEDIACORPWEB.Controllers
{
    public class ForgotPasswordController : Controller
    {
        //This will show if click "Forgot Password" email UI
        public ViewResult ForgotPasswordLoginAccount()
        {
            return View();
        }

        //This will call if click submit button of "Forgot Password" UI
        public JsonResult EmailStatusAndConfirmation(string emailAddress)
        {
            IEmailStatusAndConfirmation logicData = new ForgotPasswordDataLogic(emailAddress);
            EmailStatusAndConfirmationObjectModel data = logicData.GetEmailStatusAndConfirmation();

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        //This will provide UI for new password via email link
        public ActionResult InterfaceNewPasswordLoginAccount(string _emailID)
        {
            ViewBag.EmailID = _emailID;
            return View();
        }

        //This will accept the new password and process to DB
        public ActionResult AcceptNewPasswordLoginAccount(AcceptNewPasswordObjectModel _newPasswordData)
        {
            IAcceptNewPassword data = new AcceptNewPasswordDataLogic(_newPasswordData);

            return Json(data.AcceptNewPassword(),JsonRequestBehavior.AllowGet);
        }

        public ViewResult EmailToConfirmIsSuccessfullySent(string _emailAdd, string _firstName)
        {
            ViewBag.EmailAdd = _emailAdd;
            ViewBag.FirstName = _firstName;
            return View("_EmailToConfirmIsSuccessfullySent");
        }
    }
}