using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Model
{
    public class Address
    {
        public Address(string street)
        {
            Street = street;
        }

        public string Street { get; set; }
                
    }
}
