using BusinessRef.Abstract;
using System.Web;

namespace BusinessRef.DashBoardObject
{
    public class PersonalInformationModel : SQlErrorMessageModel
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
        public HttpPostedFileBase File { get; set; }
    }
}
