using BusinessModel.Interfaces;
using BusinessModel.Interfaces.Generics;
using BusinessModel.ObjectModel;
using BusinessModel.ObjectModel.ForgotPassword;
using BusinessModel.ObjectClass;
using DataAccess;

namespace BusinessLogic.ForgotPassword
{
    public class ForgotPasswordDataLogic : IEmailStatusAndConfirmation
    {
        private readonly string _emailAdd;
        private string _recieverEmailAddress;
        private int _emailAddressId;
        public ForgotPasswordDataLogic(string _emailAdd)
        {
            this._emailAdd = _emailAdd;
        }

        public EmailStatusAndConfirmationObjectModel GetEmailStatusAndConfirmation()
        {
            IGetDatabaseData<EmailStatusAndConfirmationObjectModel> dataAccessData = new EmailStatusAndConfirmationDataAccess(_emailAdd);

            var data = dataAccessData.GetDatabaseData();

            if (data.IsEmailExist)
            {
                _recieverEmailAddress = data.EmailAddress;
                _emailAddressId = data.EmailID;

                if (SendEmailConfirmationToNewlyOpenAccount())
                {
                    //Email Successfully sent
                    data.IsEmailSent = true;
                }
                else
                {
                    data.IsEmailSent = false;
                }

            }


            if (data.IsEmailExist && data.IsEmailSent)
            {
                //Email successfully sent
                data.StatusCodeNumber = 1;
            }

            else if (!data.IsEmailExist)
            {
                //Email are not registered!
                data.StatusCodeNumber = 2;
            }
            else
            {
                //Server error! Confirmation email not successfully sent
                data.StatusCodeNumber = 3;
            }


            return data;
        }

        private bool SendEmailConfirmationToNewlyOpenAccount()
        {
            //Call data access to get the email sender default parameter from DB
            IGetDatabaseData<EmailSenderParameterObjectModel> dataAccessData = new EmailSenderParameterDataAccess();
            EmailSenderParameterObjectModel emailSenderParameter = dataAccessData.GetDatabaseData();

            //Prepare other parameter for email constructor
            string emailBody = "Please click the link to confirm email " + "https://localhost:44362/ForgotPassword/InterfaceNewPasswordLoginAccount?_emailID=" + _emailAddressId;

            //Call email sender class and load constructor
            EmailSenderHelperClass emailSender = new EmailSenderHelperClass(emailSenderParameter, emailBody, _recieverEmailAddress);

            //Send email via sender class method
            return emailSender.IsEmailSuccessfullySent();
        }
    }
}
