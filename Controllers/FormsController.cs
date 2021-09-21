﻿using BusinessLogic.Forms;
using BusinessRef.Interfaces.Forms;
using System.Web.Mvc;

namespace xmediagraphicscorpweb.Controllers
{
    public class FormsController : Controller
    {
        public ActionResult PurchaseOrder()
        {
            IGetPurchaseOrderFormsData data = new GetPurchaseOrderDataLogic(1);
            var data1 = data.GetPurchaseOrderForms();
            return View(data1);
        }
       
        
    }
}