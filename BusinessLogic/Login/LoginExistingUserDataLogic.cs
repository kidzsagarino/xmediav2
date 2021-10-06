using BusinessRef.Interfaces.Login;
using BusinessRef.ModelObject.Login;
using BusinessRef.Interfaces.Generics;
using DataAccess.Login;
using System.Text;

namespace BusinessLogic.Login
{
    public class LoginExistingUserDataLogic : IGetLoginReturnData
    {
        private readonly LoginInfoUserModel UserLoginInfo;
        public LoginExistingUserDataLogic(LoginInfoUserModel _UserLoginInfo)
        {
            this.UserLoginInfo = _UserLoginInfo;
        }

        public LoginReturnInformationModel GetLoginReturnData()
        {
            IPostDatabaseData<LoginReturnInformationModel> dataAcccessData = new LoginReturnInformationDataAccess(UserLoginInfo);

            LoginReturnInformationModel data = dataAcccessData.PostDatabaseData();

            //If login is successful and Firstname and Lastname is not null make display name and display name initial
            if (data.LoginStatusCode.LoginStatusNumberCode == 1 && data.PersonalInfo.FirstName != null && data.PersonalInfo.LastName != null)
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
