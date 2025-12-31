using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Model
{
    public class SimulationParameters
    {
        public Client Client { get; set; }
        public string Country { get; set; }
        public Dataset SelectedDataset { get; set; }
        public List<City> SelectedCities {get; set ;}
        public uint AmountOfCustomers { get; set; }
        public uint MaxHouseNumber { get; set; }
        public uint PercentageLetters { get; set; }
        public uint MaxAge { get; set; }
        public uint MinAge { get; set; }
        

    }
}
