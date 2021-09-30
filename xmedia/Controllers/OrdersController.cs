using BusinessRef.Interfaces.Generics;
using BusinessLogic.Orders;
using BusinessRef.ModelObject.Orders;
using BusinessRef.ModelObject;
using System.Web.Mvc;

namespace xmedia.Controllers
{
    public class OrdersController : Controller
    {
        // GET: Orders
        [HttpPost]
        public JsonResult InsertUserFormOrders(UserFormOrdersModel Model)
        {
            //if hasDuplicate
            IPostDatabaseData<ConfirmInsertDataModel> data = new PostUserOrderFormsDataLogic(Model);
            
            //else
            //IPostDatabaseData<ConfirmInsertDataModel> data = new PostUserOrderFormsWithoutTVPDataLogic(Model);

            return Json(data.PostDatabaseData());
        }
    }
}