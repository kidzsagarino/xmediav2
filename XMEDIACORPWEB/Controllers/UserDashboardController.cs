using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessModel.ObjectModel.DashboardModel;
using BusinessModel.ObjectModel;
using BusinessLogic.UserDashboard;
using BusinessLogic;
using BusinessModel.Interfaces.Generics;
using BusinessModel.Interfaces;
using BusinessModel.Interfaces.UserDashboard;

namespace XMEDIACORPWEB.Controllers
{
    public class UserDashboardController : Controller
    {
        // GET: UserDashboard
        public ActionResult Index()
        {
            return View();
        }
        //User dashboard after successful login
        public ActionResult UserMainDashboard(LoginInformationFromUserObjectModel UserLoginInfo)
        {
            IGetLoginReturnDataFromDB loginReturnData = new LoginExistingUserDataLogic(UserLoginInfo);
            LoginReturnInformationObjectModel returnDataFromDB = loginReturnData.GetLoginReturnDataFormDB();

            if (returnDataFromDB.LoginStatusCode.LoginStatusNumberCode == 1)
            {
                return View(returnDataFromDB);
            }
            else
            {
                return Json(returnDataFromDB, JsonRequestBehavior.AllowGet);
            };

        }
        public PartialViewResult UserMyAccountMain()
        {
            return PartialView("_UserMyAccountMain");
        }

        public PartialViewResult UserMyAccountPersonalInfo(string userID)
        {
            IGetPersonalInformation logicData = new PersonalInformationDataLogic(userID);

            return PartialView("_UserMyAccountPersonalInfo",logicData.GetPersonalInformation());
        }

        public PartialViewResult UserMyAccountAddress()
        {
            return PartialView("_UserMyAccountAddress");
        }

        public JsonResult SaveEditedPersonalInformation(PersonalInformationObjectModel persInfoData)
        {
            ISaveMyAccountPersInfo logicData = new SaveMyAccountPersInfoDataLogic(persInfoData);
            logicData.GetDmlResultConfirmData();
            return Json(logicData.GetDmlResultConfirmData(), JsonRequestBehavior.AllowGet);
        }
    }
}