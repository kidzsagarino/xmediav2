using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRef.ModelObject
{
    public class EmailDataModel
    {
        public string Sender { get; set; }
        public string SenderPassword { get; set; }
        public string Receiver { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Root { get; set; }
        public string Attachment { get; set; }
        public string AttachmentName { get; set; }
    }
}
