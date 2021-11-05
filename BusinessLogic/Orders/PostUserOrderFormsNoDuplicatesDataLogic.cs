using BusinessRef.Interfaces.Generics;
using BusinessRef.ModelObject;
using DataAccess.Orders;
using BusinessRef.HelperObject;
using System.IO;
using System;
using System.Web;

using model = BusinessRef.ModelObject.Orders.UserFormOrdersNoDuplicateModel;

namespace BusinessLogic.Orders
{
    public class PostUserOrderFormsNoDuplicatesDataLogic : IPostDatabaseData<ConfirmInsertDataModel>
    {
        private model Model;
        private string root;
        public PostUserOrderFormsNoDuplicatesDataLogic(model DataModel, string root)
        {
            this.Model = DataModel;
            this.root = root;
        }
        public ConfirmInsertDataModel PostDatabaseData()
        {
            HttpPostedFileBase file = this.Model.Orderforms.File;

            var extension = Path.GetExtension(file.FileName);
            var datetime = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();

            var imageFileName = file.FileName + datetime + extension;
            string filepath = Path.Combine(this.root, imageFileName);

            if(FileUploadHelper.File(file, filepath) == true)
            {
                this.Model.Orderforms.ImageFilename = imageFileName;

                IPostDatabaseData<ConfirmInsertDataModel> returnData = new UserFormsOrderNoDuplicatesDataAccess(this.Model);
                ConfirmInsertDataModel returnValue = returnData.PostDatabaseData();

                return returnValue;

            }

            return new ConfirmInsertDataModel() { IsSuccessful = false, HasError = true, ErrorMessage = "File must not greater than 5mb." };

        }
    }
}
