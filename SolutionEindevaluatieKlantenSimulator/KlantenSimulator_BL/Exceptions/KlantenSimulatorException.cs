using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Exceptions
{
    public class KlantenSimulatorException : Exception
    {
        
            public KlantenSimulatorException(string? message, Exception? innerException) : base(message, innerException) { }
            public KlantenSimulatorException(string? message) : base(message) { }



        
    }
}
