using BusinessLogic.Forms;
using BusinessRef.Interfaces.Forms;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using System;
using BusinessRef.ModelObject.Forms;

namespace xmedia.Controllers
{
    public class FormsController : Controller
    {
        // GET: Forms
        public ActionResult PurchaseOrder()
        {
            IGetPurchaseOrderFormsData data = new GetPurchaseOrderDataLogic(1);
            var data1 = data.GetPurchaseOrderForms();

            return View("~/Views/Forms/Index.cshtml",data1);
        }

        public ActionResult PurchaseOrder1()
        {
            GetPurchaseOrderFormsInitialDataModel model = null;

            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44308/api/");

                var responseTask = client.GetAsync("forms/purchaseorder");

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<GetPurchaseOrderFormsInitialDataModel>();

                    readTask.Wait();

                    model = readTask.Result;
                }
            }

            return View("~/Views/Forms/Index.cshtml", model);
        }
        
    }

}