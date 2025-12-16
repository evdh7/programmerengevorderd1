using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VakantieparkBL.Exceptions
{
    public class VakantieparkException : Exception
    {
        public VakantieparkException()
        {
        }

        public VakantieparkException(string? message) : base(message)
        {
        }

        public VakantieparkException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
