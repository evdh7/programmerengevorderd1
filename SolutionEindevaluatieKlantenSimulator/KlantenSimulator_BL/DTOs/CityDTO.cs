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
        public int? Id { get; set; }
        public string Name { get; set; }
        public HashSet<string> Addresses { get; set; }

        // Constructor 1 — name only
        public CityDTO(string name)
        {
            Name = name;
            Addresses = [];
        }

        // Constructor 2 — id + name
        public CityDTO(int id, string name)
        {
            Id = id;
            Name = name;
            Addresses = [];
        }

        public override string ToString()
        {
            return Name;
        }
    }

}
