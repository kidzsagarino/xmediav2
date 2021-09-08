using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModel.Interfaces.Generics;
using BusinessModel.Abstract;

namespace BusinessModel.ObjectModel
{
    public class DmlInsertNewUserAccountObjectModel:ErrorStatus
    {
        public bool IsSuccessful { get; set; } = false;
        public string FirstName { get; set; }
        public bool HasExistingEmail { get; set; }
        public int EmailAddressId { get; set; }
        public string EmailAddress { get; set; }
        public bool IsEmailSent { get; set; }

    }
}
