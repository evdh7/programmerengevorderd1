using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Model
{
    public class Country
    {
        public Country(string name, List<City> cities)
        {
            Name = name;
            Cities = cities;
        }

        public string Name { get; set; }
        public List<City> Cities { get; set; } = new();
    }
}
