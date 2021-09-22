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
        public int FormSizeID { get; set; }
        public string FormSizes { get; set; }
        public float FormSizeFactor { get; set; }
    }
}
