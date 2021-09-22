using BusinessRef.Interfaces.Forms;
using BusinessRef.Interfaces.Generics;
using DataAccess.Forms;
using model = BusinessRef.ModelObject.Forms.GetPurchaseOrderFormsInitialDataModel;

namespace BusinessLogic.Forms
{
    public class GetPurchaseOrderDataLogic : IGetPurchaseOrderFormsData
    {
        private int ID;
        public GetPurchaseOrderDataLogic(int ID)
        {
            this.ID = ID;

        }
        public model GetPurchaseOrderForms()
        {
            IGetDatabaseData<model> data = new FormDataAccess(this.ID);
            return data.GetDatabaseData();
        }
    }
}
