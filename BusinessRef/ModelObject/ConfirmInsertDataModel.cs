
using BusinessRef.Abstract;

namespace BusinessRef.ModelObject
{
    public class ConfirmInsertDataModel : SQlErrorMessageModel
    {
        public bool IsSuccessful { get; set; }
    }
}
