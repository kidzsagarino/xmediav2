using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModel.Abstract;

namespace BusinessModel.ObjectModel.ForgotPassword
{
    public class DmlInsertNewPassword:ErrorStatus
    {
        public bool IsSuccessful { get; set; }
        public int StatusCodeNumber { get; set; }
    }
}
