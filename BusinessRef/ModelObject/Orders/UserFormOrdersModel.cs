
using System;
using System.Collections.Generic;

namespace BusinessRef.ModelObject.Orders
{
    public class UserFormOrdersModel
    {
        public int ID { get; set; }
        public int OrderFormsCategory_ID { get; set; }
        public int UsersID { get; set; }
        public DateTime OrderDate { get; set; }
        public float TotalPrice { get; set; }
        public List<OrderFormsModel> Orderforms { get; set; }
        public List<OrderFormDuplicatesModel> OrderFormDuplicates { get; set; }
    }
}
