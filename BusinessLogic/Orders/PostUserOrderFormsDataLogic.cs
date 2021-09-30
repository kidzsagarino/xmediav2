
using BusinessRef.Interfaces.Generics;
using BusinessRef.ModelObject;
using DataAccess.Orders;

using model = BusinessRef.ModelObject.Orders.UserFormOrdersModel;

namespace BusinessLogic.Orders
{
    public class PostUserOrderFormsDataLogic : IPostDatabaseData<ConfirmInsertDataModel>
    {
        private model Model;
        public PostUserOrderFormsDataLogic(model DataModel)
        {
            this.Model = DataModel;
        }
        public ConfirmInsertDataModel PostDatabaseData()
        {
            IPostDatabaseData<ConfirmInsertDataModel> returnData = new UserFormsOrderDataAccess(this.Model);
            ConfirmInsertDataModel returnValue = returnData.PostDatabaseData();

            return returnValue;
        }
    }
}
