using BusinessRef.Interfaces.Forms;
using BusinessRef.Interfaces.Generics;
using DataAccess.Forms;
using model = BusinessRef.ModelObject.Forms.GetPurchaseOrderFormsInitialDataModel;

namespace BusinessLogic.Forms
{
    public class FormCommonDataLogic : IGetFormCommonData
    {
        public model GetFormCommon()
        {
            IGetDatabaseData<model> data = new FormCommonDataAccess();
            return data.GetDatabaseData();
        }
    }
}
