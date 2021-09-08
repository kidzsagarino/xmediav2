using System.Net;
using System.Net.Mail;
using BusinessModel.ObjectModel;

namespace BusinessModel.ObjectClass
{
    public class EmailSenderHelperClass
    {
        
        private readonly EmailSenderParameterObjectModel emailSendParameter;
        private readonly string recieverEmail;
        private readonly string emailBody;

        public EmailSenderHelperClass(EmailSenderParameterObjectModel _emailSendParameter, string _emailBody, string _recieverEmail)
        {
            this.emailSendParameter = _emailSendParameter;
            this.recieverEmail = _recieverEmail;
            this.emailBody = _emailBody;
        }

        public bool IsEmailSuccessfullySent()
        {

            MailMessage message = new MailMessage(emailSendParameter.SenderEmail, recieverEmail, emailSendParameter.EmailSubject, emailBody);

            SmtpClient client = new SmtpClient
            {
                Host = emailSendParameter.EmailHost,
                Port = emailSendParameter.PortNumber,
                EnableSsl = emailSendParameter.EnableSsl,
                UseDefaultCredentials = emailSendParameter.UseDefaultCredentials,
                Credentials = new NetworkCredential(emailSendParameter.SenderEmail, emailSendParameter.SenderPassword)
            };

            try
            {
                client.Send(message);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
