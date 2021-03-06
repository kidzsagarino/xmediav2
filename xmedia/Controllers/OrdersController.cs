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
        public JsonResult InsertUserFormOrders(UserFormOrdersNoDuplicateModel Model)
        {
            string root = Server.MapPath("~/files/companyLogo");

            IPostDatabaseData<ConfirmInsertDataModel> data = new PostUserOrderFormsNoDuplicatesDataLogic(Model, root);
            return Json(data.PostDatabaseData(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertUserFormOrdersWithDuplicates(UserFormOrdersModel Model)
        {
            IPostDatabaseData<ConfirmInsertDataModel> data = new PostUserOrderFormsDataLogic(Model);
            return Json(data.PostDatabaseData(), JsonRequestBehavior.AllowGet);
        }
    }
}