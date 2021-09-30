using BusinessRef.Interfaces.Generics;
using BusinessRef.ModelObject;
using DataAccess.Orders;

using model = BusinessRef.ModelObject.Orders.UserFormOrdersModel;

namespace BusinessLogic.Orders
{
    public class PostUserOrderFormsWithoutTVPDataLogic : IPostDatabaseData<ConfirmInsertDataModel>
    {
        private model Model;
        public PostUserOrderFormsWithoutTVPDataLogic(model DataModel)
        {
            this.Model = DataModel;
        }
        public ConfirmInsertDataModel PostDatabaseData()
        {
            IPostDatabaseData<ConfirmInsertDataModel> returnData = new UserFormsOrderWithoutTVPDataAccess(this.Model);
            ConfirmInsertDataModel returnValue = returnData.PostDatabaseData();

            return returnValue;
        }
    }
}
