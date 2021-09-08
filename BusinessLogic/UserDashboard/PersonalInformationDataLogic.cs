using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModel.Interfaces.UserDashboard;
using BusinessModel.Interfaces.Generics;
using BusinessModel.ObjectModel.DashboardModel;
using DataAccess.DashboardDataAccess;

namespace BusinessLogic.UserDashboard
{
    public class PersonalInformationDataLogic : IGetPersonalInformation
    {
        private readonly int _userID;
        public PersonalInformationDataLogic(string _userID)
        {
            this._userID =Convert.ToInt32(_userID);
        }
        public PersonalInformationObjectModel GetPersonalInformation()
        {
            IGetDatabaseData<PersonalInformationObjectModel> dataAccessData = new GetPersonalInformationDataAccess(_userID);
            return dataAccessData.GetDatabaseData();
        }
    }
}
