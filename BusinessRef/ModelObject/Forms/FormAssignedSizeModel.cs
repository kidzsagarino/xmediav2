using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRef.ModelObject.Forms
{
    public class FormAssignedSizeModel
    {
        public int ID { get; set; }
        public int FormMasterID { get; set; }
        public int FormSizeID { get; set; }
        public string PaperSize { get; set; }
        public float DivisorFactor { get; set; }
        public float LaborFactor { get; set; }
    }
}
