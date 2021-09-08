using System;

namespace BusinessModel.Abstract
{
    public abstract class ErrorStatus
    {
        public bool hasError { get; set; } = false;
        public String ErrorMessage { get; set; }
    }
}
