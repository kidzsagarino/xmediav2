using BusinessLogic.Forms;
using BusinessRef.Interfaces.Forms;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using System;
using BusinessRef.ModelObject.Forms;
using System.Threading.Tasks;

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

        public async Task<ActionResult> PurchaseOrder1()
        {
            GetPurchaseOrderFormsInitialDataModel model = null;

            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44309/api/");
                
                var responseTask = await client.GetAsync("forms/GetDropdownData");

                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = await responseTask.Content.ReadAsAsync<GetPurchaseOrderFormsInitialDataModel>();

                    model = readTask;
                }
            }

            return View("~/Views/Forms/Index.cshtml", model);
        }

        
    }

} 