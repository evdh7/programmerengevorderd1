using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Model
{
    public class City
    {
        public City(string name, List<Address> addresses)
        {
            Name = name;
            Addresses = addresses;
        }

        public string Name { get; set; }
        public List<Address> Addresses { get; set; } = new();
    }
}
