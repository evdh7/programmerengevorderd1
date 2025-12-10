using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KlantenSimulatorBL.DTOs
{
    public class CityDTO
    {
        public CityDTO(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public List<string>? Addresses { get; set; } = new();
    }
}
