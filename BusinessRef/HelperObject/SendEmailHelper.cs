using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using BusinessRef.ModelObject;

namespace BusinessRef.HelperObject
{
    public class SendEmailHelper
    {
        private EmailDataModel email;
        public SendEmailHelper(EmailDataModel model)
        {
            this.email = model;
        }
        public bool Send()
        {
            var password = this.email.SenderPassword;
            var sub = this.email.Subject;
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(this.email.Sender, password)
                
            };

            MailMessage mess = new MailMessage(this.email.Sender, this.email.Receiver, sub, this.email.Message);
            mess.IsBodyHtml = true;

            if (this.email.Attachment != null)
            {
                string file = this.email.Root + this.email.Attachment;

                Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
                data.Name = this.email.AttachmentName;

                mess.Attachments.Add(data);
            }

            try
            {
                smtp.Send(mess);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
