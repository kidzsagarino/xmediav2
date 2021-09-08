using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModel.Interfaces.UserDashboard;
using BusinessModel.Interfaces.Generics;
using BusinessModel.ObjectModel.DashboardModel;
using BusinessModel.ObjectModel;
using DataAccess;

namespace BusinessLogic.UserDashboard
{
    public class SaveMyAccountPersInfoDataLogic : ISaveMyAccountPersInfo
    {
        private readonly PersonalInformationObjectModel _persInfoData;
        public SaveMyAccountPersInfoDataLogic(PersonalInformationObjectModel _persInfoData)
        {
            if(_persInfoData.BirthDate != null)
            {
                StringBuilder newDateFormat = new StringBuilder();

                newDateFormat.Append(_persInfoData.BirthDate.Substring(5, 2));
                newDateFormat.Append("/");
                newDateFormat.Append(_persInfoData.BirthDate.Substring(8, 2));
                newDateFormat.Append("/");
                newDateFormat.Append(_persInfoData.BirthDate.Substring(0, 4));

                _persInfoData.BirthDate = newDateFormat.ToString();
            }
            

            this._persInfoData = _persInfoData;
        }
        
        public DmlReturnDataFromDbObjectModel<bool> GetDmlResultConfirmData()
        {
            IPostDatabaseData<DmlReturnDataFromDbObjectModel<bool>> dataAccessData = new SaveEditedPersInfoDataAccess(_persInfoData);
            return dataAccessData.PostDatabaseData();
        }
    }
}
