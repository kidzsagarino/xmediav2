using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModel.Abstract;

namespace BusinessModel.ObjectModel
{
    public class DmlReturnDataFromDbObjectModel<T>:ErrorStatus
    {
        public T DmlReturnData { get; set; }
    }
}
