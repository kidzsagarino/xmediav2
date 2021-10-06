namespace BusinessRef.ModelObject.Orders
{
    public class OrderFormsModel
    {
        public int ID { get; set; }
        public int UserOrderForms_ID { get; set; }
        public int FormsMasterData_ID { get; set; }
        public int FormsPaperSizesRef_ID { get; set; }
        public int PaperTypeRef_ID { get; set; }
        public int PaperColorRef_ID { get; set; }
        public int PaddingGlue_ID { get; set; }
        public bool hasPaddingGlue { get; set; }
        public int NoOfSetPad { get; set; }
        public string PadSide { get; set; }
        public float UnitPrice { get; set; }
        public int Quantity { get; set; }
        public bool hasDuplicate { get; set; }
    }
}
