using BusinessRef.Abstract;
using System;

namespace BusinessRef.ModelObject.Orders
{
    public class UserFormOrdersNoDuplicateModel : SQlErrorMessageModel
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int UsersID { get; set; }
        public DateTime OrderDate { get; set; }
        public float TotalPrice { get; set; }
        public int OrderStatusID { get; set; }
        public OrderFormsModel Orderforms { get; set; }
    }
}
