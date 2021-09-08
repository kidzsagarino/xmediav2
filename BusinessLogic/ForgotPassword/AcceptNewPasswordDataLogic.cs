using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModel.Interfaces.ForgotPassword;
using BusinessModel.ObjectModel.ForgotPassword;
using DataAccess.ForgotPasswordDataAccess;

namespace BusinessLogic.ForgotPassword
{
    public class AcceptNewPasswordDataLogic : IAcceptNewPassword
    {
        private readonly AcceptNewPasswordObjectModel _newPassData;
        public AcceptNewPasswordDataLogic(AcceptNewPasswordObjectModel _newPassData)
        {
            this._newPassData = _newPassData;
        }
        public DmlInsertNewPassword AcceptNewPassword()
        {
            AcceptNewPasswordDataAccess dataAccessData = new AcceptNewPasswordDataAccess(_newPassData);
            return dataAccessData.PostDatabaseData();
        }
    }
}
