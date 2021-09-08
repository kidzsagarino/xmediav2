using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModel.ObjectModel.ForgotPassword;

namespace BusinessModel.Interfaces.ForgotPassword
{
    public interface IAcceptNewPassword
    {
        DmlInsertNewPassword AcceptNewPassword();
    }
}
