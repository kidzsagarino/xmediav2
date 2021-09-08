using System.Web.Mvc;
using BusinessModel.ObjectModel;
using BusinessModel.Interfaces;
using BusinessLogic;
using System.Collections.Specialized;

namespace XMEDIACORPWEB.Controllers
{
    public class NewUserAccountController : Controller
    {

        //Fill-up forms for new user application
        public ActionResult NewUserFillUpForms()
        {
            return View();
        }

        //Create new user save to DB and confirmed if successful or not successful
        //This will redirect to other action method depending if success or not
        [HttpPost]
        public ActionResult CreateUsersAccount(NewAccountInfoObjectModel newUserData)
        {
            ISaveNewUserAccountToServerAndDB logicData = new NewUserAccountDataLogic(newUserData);
            DmlInsertNewUserAccountObjectModel dmlData = logicData.SaveNewUserAccountToServerAndDB();

            if (dmlData.IsSuccessful)
            {
                return NewAccountSuccessConfirmation(dmlData);
            }
            else if (dmlData.HasExistingEmail)
            {
                return NewAccountHasExistingEmailAddress(dmlData);
            }
            else 
            {
                return NewAccountFailedConfirmation();
            }

        }

        //This partial view is for success confirmation of new account 
        private PartialViewResult NewAccountSuccessConfirmation(DmlInsertNewUserAccountObjectModel dmlData)
        {
            return PartialView("_NewAccountSuccessConfirmation", dmlData);
        }

        public PartialViewResult NewAccountHasExistingEmailAddress(DmlInsertNewUserAccountObjectModel dmlData)
        {
            return PartialView("_NewAccountHasExistingEmailAddress", dmlData);
        }
        //This partial view is for failed confirmation of new account 
        private PartialViewResult NewAccountFailedConfirmation()
        {
            return PartialView("_NewAccountFailedConfirmation");
        }


        //User will login here after creating account 
        public PartialViewResult UserLoginInterface()
        {
            return PartialView("_UserLoginInterface");
        }

        

        [HttpGet]
        public ActionResult ConfirmNewUserAccountEmail(string _emailID)
        {
            IConfirmEmailAddressInDatabase emailConfirmation = new EmailConfirmationDataLogic(_emailID);

            DmlInsertNewUserAccountObjectModel cofirm = emailConfirmation.ConfirmUserEmailAddress();

            if (cofirm.hasError == false)
            {

                return PartialView("_EmailConfirmSuccess");
            }
            else
            {
                return PartialView("_NewAccountFailedConfirmation");
            }

            
        }

     
      
    }
}