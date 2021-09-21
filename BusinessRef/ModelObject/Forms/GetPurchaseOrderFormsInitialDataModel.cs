using BusinessRef.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRef.ModelObject.Forms
{
    public class GetPurchaseOrderFormsInitialDataModel : SQlErrorMessageModel
    {
        public ICollection<FormPaddingCostModel> formPaddings { get; set; }
        public ICollection<FormPaperColorModel> formPaperColors { get; set; }
        public ICollection<FormPaperQuantityModel> formPaperQuantities { get; set; }
        public ICollection<FormPaperTypeModel> formPaperTypes { get; set; }
        public ICollection<FormsAssignedSizeModel> formsAssignedSizes { get; set; }
    }
}
