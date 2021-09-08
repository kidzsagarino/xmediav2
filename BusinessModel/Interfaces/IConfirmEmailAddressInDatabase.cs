
using BusinessModel.ObjectModel;
namespace BusinessModel.Interfaces
{
    public interface IConfirmEmailAddressInDatabase
    {
        DmlInsertNewUserAccountObjectModel ConfirmUserEmailAddress();
    }
}
