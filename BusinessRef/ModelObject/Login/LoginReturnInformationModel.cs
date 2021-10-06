using BusinessRef.Abstract;
using BusinessRef.DashBoardObject;

namespace BusinessRef.ModelObject.Login
{
    public class LoginReturnInformationModel : SQlErrorMessageModel
    {
        public LoginStatusAndResultsModel LoginStatusCode { get; set; }
        public PersonalInformationModel PersonalInfo { get; set; }
        public string DisplayName { get; set; }
        public string NameInitial { get; set; }
    }
}
