using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModel.Abstract;

namespace BusinessModel.ObjectModel.DashboardModel
{
    public class PersonalInformationObjectModel:ErrorStatus
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string GenderID { get; set; }
        public string BirthDate { get; set; }
        public string Citizenship { get; set; }
        public string Profession { get; set; }
        public string PhotoImageFileName { get; set; }
    }
}
