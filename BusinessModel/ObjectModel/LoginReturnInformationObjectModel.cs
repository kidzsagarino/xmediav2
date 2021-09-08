using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModel.Abstract;
using BusinessModel.ObjectModel.DashboardModel;

namespace BusinessModel.ObjectModel
{
    public class LoginReturnInformationObjectModel:ErrorStatus
    {
        public LoginStatusAndResultsObjectModel LoginStatusCode { get; set; }

        public PersonalInformationObjectModel PersonalInfo { get; set; }
        public string DisplayName { get; set; }
        public string NameInitial { get; set; }
    }
}
