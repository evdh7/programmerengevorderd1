using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Model
{
    public class City(string name)
    {
        public int CityId {  get; set; }
        public string Name { get; set; } = name;
        public List<Address> Addresses { get; set; } = [];
        public override string ToString() 
        {
            return Name; 
        }
    }
}
