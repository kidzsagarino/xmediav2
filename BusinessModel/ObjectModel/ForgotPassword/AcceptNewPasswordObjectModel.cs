using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.ObjectModel.ForgotPassword
{
    public class AcceptNewPasswordObjectModel
    {
        public string EmailID { get; set; }
        public string NewPassword { get; set; }
    }
}
