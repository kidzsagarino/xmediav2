using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRef.ModelObject.Orders
{
    public class UserFormOrdersNoDuplicateModel
    {
        public int ID { get; set; }
        public int OrderFormsCategory_ID { get; set; }
        public int UsersID { get; set; }
        public DateTime OrderDate { get; set; }
        public float TotalPrice { get; set; }
        public OrderFormsModel Orderforms { get; set; }
    }
}
