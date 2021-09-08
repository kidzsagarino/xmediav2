using BusinessModel.Interfaces;
using BusinessModel.Interfaces.Generics;
using BusinessModel.ObjectClass;
using BusinessModel.ObjectModel;
using DataAccess;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Collections.Specialized;

namespace BusinessLogic
{
    public class NewUserAccountDataLogic : ISaveNewUserAccountToServerAndDB
    {
        private readonly NewAccountInfoObjectModel _newUserData;

        private string recieverEmailAddress;
        private int emailAddressId;
        public NewUserAccountDataLogic(NewAccountInfoObjectModel _newUserData)
        {
            this._newUserData = _newUserData;
        }

        public DmlInsertNewUserAccountObjectModel SaveNewUserAccountToServerAndDB()
        {
            //Set photo image file name
            _newUserData.PhotoImageFileName = SaveImageToServerAndReturnFileName();

            //Call data access to POST data and get return value
            IPostDatabaseData<DmlInsertNewUserAccountObjectModel> dataAccess = new NewUserAccountDataAccess(_newUserData);
            DmlInsertNewUserAccountObjectModel data = dataAccess.PostDatabaseData();


            //Check if has error, if no error send email for confirmation
            if (data.hasError)
            {
                //Do something here in the future
            }
            else if (data.HasExistingEmail)
            {
                //Do something here in the future
            }
            else
            {
                recieverEmailAddress = data.EmailAddress;
                emailAddressId = data.EmailAddressId;
                data.IsEmailSent = SendEmailConfirmationToNewlyOpenAccount();
            }

            return data;
        }

        
        private string SaveImageToServerAndReturnFileName()
        {
            try
            {
                string fileNameWithExtension;
                //string fileExtension;
                //Check if there is an image file
                if (!(_newUserData.ImageFile is null))
                {
                    HttpPostedFileBase userPostedFile = _newUserData.ImageFile;
                    string fileName = Path.GetFileName(userPostedFile.FileName);
                    string fileExt = ".jpg";
                    fileNameWithExtension = fileName + fileExt;
                    string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~"), "images", "userphotoimage", fileNameWithExtension);

                    userPostedFile.SaveAs(filePath);
                }
                else
                {
                    fileNameWithExtension = null;
                }

                return fileNameWithExtension;
            }
            catch
            {
                return null;
            }

        }

        private bool SendEmailConfirmationToNewlyOpenAccount()
        {
            //Call data access to get the email sender default parameter from DB
            IGetDatabaseData<EmailSenderParameterObjectModel> dataAccessData = new EmailSenderParameterDataAccess();
            EmailSenderParameterObjectModel emailSenderParameter = dataAccessData.GetDatabaseData();

            //Prepare other parameter for email constructor
            string emailBody = "Please click the link to confirm email " + "https://localhost:44362/NewUserAccount/ConfirmNewUserAccountEmail?_emailID=" + emailAddressId;

            //Call email sender class and load constructor
            EmailSenderHelperClass emailSender = new EmailSenderHelperClass(emailSenderParameter, emailBody, recieverEmailAddress);

            //Send email via sender class method
            return emailSender.IsEmailSuccessfullySent();
        }

        
    }
}
