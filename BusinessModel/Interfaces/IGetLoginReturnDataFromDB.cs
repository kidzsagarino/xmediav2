using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModel.ObjectModel;

namespace BusinessModel.Interfaces
{
    public interface IGetLoginReturnDataFromDB
    {
        LoginReturnInformationObjectModel GetLoginReturnDataFormDB();
    }
}
