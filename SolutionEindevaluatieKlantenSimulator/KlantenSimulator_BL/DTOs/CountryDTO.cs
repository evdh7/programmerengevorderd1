using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.DTOs
{
    public class CountryDTO
    {
        public CountryDTO(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public List<CityDTO> Cities { get; set; } = new();
    }
}
