using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModel.Abstract;

namespace BusinessModel.ObjectModel.ForgotPassword
{
    public class EmailStatusAndConfirmationObjectModel:ErrorStatus
    {
        public string FirstName { get; set; }
        public int EmailID { get; set; }
        public string EmailAddress { get; set; }
        public bool IsEmailExist { get; set; }
        public bool IsEmailSent { get; set; }
        public int StatusCodeNumber { get; set; }
        
    }
}
