using BusinessRef.Abstract;

namespace BusinessRef.ModelObject.Account
{
    public class LoginInfoUserModel : SQlErrorMessageModel
    {
        public string EmailAddress { get; set; }
        public string IStillLoveYou { get; set; }
    }
}
