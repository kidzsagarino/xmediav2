using System.Collections.Generic;
using System.Linq;
using System;
using BusinessModel.Interfaces;
using BusinessModel.Interfaces.Generics;
using BusinessModel.ObjectModel;
using DataAccess;

namespace BusinessLogic
{
    public class EmailConfirmationDataLogic : IConfirmEmailAddressInDatabase
    {
        private readonly int _emailId;
        public EmailConfirmationDataLogic(string _emailId)
        {
            this._emailId = Convert.ToInt32(_emailId);
        }
        public DmlInsertNewUserAccountObjectModel ConfirmUserEmailAddress()
        {
            IPostDatabaseData<DmlInsertNewUserAccountObjectModel> dataAccess = new EmailConfirmationDataAccess(_emailId);
           
            DmlInsertNewUserAccountObjectModel data = dataAccess.PostDatabaseData();

            return data;
        }
    }
}
