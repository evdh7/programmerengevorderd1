using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KlantenSimulatorBL.DTOs
{
    public class CityDTO(string name)
    {
        public string Name { get; set; } = name;
        public HashSet<string>? Addresses { get; set; } = [];
    }
}
