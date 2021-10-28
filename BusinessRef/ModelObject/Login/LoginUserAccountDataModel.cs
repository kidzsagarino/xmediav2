using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using BusinessRef.Abstract;
using BusinessRef.DashBoardObject;

namespace BusinessRef.ModelObject.Login
{
    public class LoginUserAccountDataModel: SQlErrorMessageModel
    {
        public LoginInfoUserModel LoginInfo { get; set; }
        public PersonalInformationModel PersonalInfo { get; set; }
        public HttpPostedFileBase File { get; set; }
        public string MobileNo { get; set; }
        public string LandlineNo { get; set; }
        public string Company { get; set; }
    }
}
