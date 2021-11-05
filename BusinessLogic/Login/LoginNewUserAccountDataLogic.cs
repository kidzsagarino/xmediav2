using BusinessRef.Interfaces.Login;
using BusinessRef.ModelObject.Login;
using BusinessRef.Interfaces.Generics;
using DataAccess.Login;
using System.Text;
using System.Web;
using System.IO;
using System;
using BusinessRef.HelperObject;

namespace BusinessLogic.Login
{
    public class LoginNewUserAccountDataLogic : IGetLoginReturnData
    {
        private LoginNewUserAccountDataModel model;
        private string root;

        public LoginNewUserAccountDataLogic(LoginNewUserAccountDataModel model, string root)
        {
            this.model = model;
            this.root = root;
        }

        public LoginReturnInformationModel GetLoginReturnData()
        {
            HttpPostedFileBase file = this.model.PersonalInfo.File;

            var extension = Path.GetExtension(file.FileName);
            var datetime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();

            var imageFileName = "photo" + datetime + extension;
            string filepath = Path.Combine(this.root, imageFileName);
            this.model.PersonalInfo.PhotoImageFileName = imageFileName;

            IPostDatabaseData<LoginReturnInformationModel> returnData = new LoginNewUserAccountDataAccess(this.model);
            LoginReturnInformationModel returnValue = returnData.PostDatabaseData();

            if(returnValue.HasExistingEmail == false)
            {
                if(FileUploadHelper.File(file, filepath))
                {
                    return returnValue;
                }
                else
                {
                    return new LoginReturnInformationModel() { IsSuccessful = false, HasError = true, ErrorMessage = "File must not greater than 5mb" };
                }
            }

            return returnValue; 
        }
    }
}
