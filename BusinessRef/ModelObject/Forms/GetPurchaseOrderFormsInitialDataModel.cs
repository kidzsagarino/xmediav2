using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessRef.Abstract;

namespace BusinessRef.ModelObject.Forms
{
    public class GetPurchaseOrderFormsInitialDataModel : SQlErrorMessageModel
    {
        public ICollection<FormPaddingCostModel> FormPaddings { get; set; }
        public ICollection<FormPaperColorModel> FormPaperColors { get; set; }
        public ICollection<FormQuantityModel> FormQuantities { get; set; }
        public ICollection<FormPaperTypeModel> FormPaperTypes { get; set; }
        public ICollection<FormAssignedSizeModel> FormAssignedSizes { get; set; }
        public ICollection<FormPrintColorModel> FormPrintColors { get; set; }
    }
}
