using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Model
{
    public class Country(string name, List<City> cities)
    {
        public string Name { get; set; } = name;
        public List<City> Cities { get; set; } = cities;
    }
}
