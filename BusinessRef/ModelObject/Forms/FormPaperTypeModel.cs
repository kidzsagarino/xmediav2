using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRef.ModelObject.Forms
{
    public class FormPaperTypeModel
    {
        public int ID { get; set; }
        public int FormMasterID { get; set; }
        public string PaperType { get; set; }
        public string Thickness { get; set; }
        public float PaperCostAtA3 { get; set; }
        public float LaborCostAtA3 { get; set; }
        public float PrintingCostAtA3BW { get; set; }
        public float PrintingCostAtA3Colored { get; set; }
    }
}
