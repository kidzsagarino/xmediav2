using BusinessRef.Abstract;
namespace BusinessRef.HelperObject
{
    public class PadSideDataModel : SQlErrorMessageModel
    {
        public int ID { get; set; }
        public string SideName { get; set; }
        public bool isDeleted { get; set; }

    }
}
