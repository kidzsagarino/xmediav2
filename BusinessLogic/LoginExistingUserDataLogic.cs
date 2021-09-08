using System;
using System.Collections.Generic;
using System.Linq;
using BusinessModel.Interfaces;
using BusinessModel.Interfaces.Generics;
using BusinessModel.ObjectModel;
using DataAccess;
using System.Web;
using System.IO;
using System.Text;

namespace BusinessLogic
{
    public class LoginExistingUserDataLogic : IGetLoginReturnDataFromDB
    {
        private readonly LoginInformationFromUserObjectModel UserLoginInfo;

       
        public LoginExistingUserDataLogic(LoginInformationFromUserObjectModel _UserLoginInfo)
        {
            UserLoginInfo = _UserLoginInfo;
        }
        public LoginReturnInformationObjectModel GetLoginReturnDataFormDB()
        {
            IPostDatabaseData<LoginReturnInformationObjectModel> dataAcccessData = new LoginReturnInformationDataAccess(UserLoginInfo);

            LoginReturnInformationObjectModel data = dataAcccessData.PostDatabaseData();
            
            //If login is successful and Firstname and Lastname is not null make display name and display name initial
            if(data.LoginStatusCode.LoginStatusNumberCode == 1 && data.PersonalInfo.FirstName != null && data.PersonalInfo.LastName!=null)
            {
                StringBuilder NameDisplay = new StringBuilder();
                NameDisplay.Append("Hi ");
                NameDisplay.Append(data.PersonalInfo.FirstName);
                NameDisplay.Append(" ");
                NameDisplay.Append(data.PersonalInfo.LastName);
                data.DisplayName = NameDisplay.ToString();

                StringBuilder NameInitial = new StringBuilder();
                NameInitial.Append(data.PersonalInfo.FirstName.Substring(0, 1));
                NameInitial.Append(data.PersonalInfo.LastName.Substring(0, 1));
                data.NameInitial = NameInitial.ToString();
            }
            

            return data;
        }
    }
}
