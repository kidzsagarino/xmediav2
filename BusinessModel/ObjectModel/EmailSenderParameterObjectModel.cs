using BusinessModel.Abstract;

namespace BusinessModel.ObjectModel
{
    public class EmailSenderParameterObjectModel : ErrorStatus
    {
        public string SenderEmail { get; set; }
        public string SenderPassword { get; set; }
        public string EmailSubject { get; set; }
        public string EmailHost { get; set; }
        public int PortNumber { get; set; }
        public bool EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }
    }
}
