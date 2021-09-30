namespace BusinessRef.ModelObject.Orders
{
    public class OrderFormDuplicatesModel
    {
        public int ID { get; set; }
        public int OrderForms_ID { get; set; }
        public int FormsPaperSizesRef_ID { get; set; }
        public int PaperTypeRef_ID { get; set; }
        public int PaperColorRef_ID { get; set; }
        public float UnitPrice { get; set; }
        public bool isOriginal { get; set; }
    }
}
