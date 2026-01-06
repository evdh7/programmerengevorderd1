using KlantenSimulatorBL.DTOs;
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
        public string CountryName { get; set; }
        public int CountryId { get; set; }  
        public Dataset SelectedDataset { get; set; }
        public List<CityDTO> SelectedCities { get; set; }
        public int AmountOfCustomers { get; set; }
        public int MaxHousenumber { get; set; }
        public int PercentageLetters { get; set; }
        public int MaxAge { get; set; }
        public int MinAge { get; set; }
        public bool HasLinkedStreetsAndCities =>
            CountryName!="CzechRepublic";
    }
}