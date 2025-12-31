using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.DTOs
{
    public class CountryDTO(string name)
    {
        public string Name { get; set; } = name;
        public List<CityDTO> Cities { get; set; } = [];
        public HashSet<string>? Addresses { get; set; } = [];

    }
}
