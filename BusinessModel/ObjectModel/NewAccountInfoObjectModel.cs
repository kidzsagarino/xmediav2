using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.ObjectModel
{
    public class NewAccountInfoObjectModel
    {
        public string EmailAddress { get; set; }
        public string IStillLoveYou { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string LandlineNo { get; set; }
        public string MobileNo { get; set; }
        public string PhotoImageFileName { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }

    }
}
