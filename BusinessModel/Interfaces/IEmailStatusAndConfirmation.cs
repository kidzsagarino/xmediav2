using BusinessModel.ObjectModel.ForgotPassword;

namespace BusinessModel.Interfaces
{
    public interface IEmailStatusAndConfirmation
    {
        EmailStatusAndConfirmationObjectModel GetEmailStatusAndConfirmation();
    }
}
