using KlantenSimulatorBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Manager
{
    public class AddressSimulator
    {
        private Random r = new Random();
        //private List<string> straatnamen = new();
        //private List<(int postcode, string gemeente)> postcodeGemeente = new();
        //private int maxHuisnummer;
        //private int percentLetter;

        public List<string> StreetNames { get; set; }
        List<(int cityId, string cityName)> CityIdCityName { get; set; }
        public int MaxHousenumber { get; set; }
        public int? PercentLetter { get; set; }

        public AddressSimulator(List<string> streetnames, List<(int cityId, string gemeente)> cityIdCityName, int maxHousenumber, int? percentLetter)
        {
            StreetNames = streetnames;
            CityIdCityName = cityIdCityName;
            MaxHousenumber = maxHousenumber;
            PercentLetter = percentLetter;
        }
        public List<Address> GetAddresses(int aantal)
        {
            List<Address> addresses = new();
            int n = 0;
            while (n < aantal)
            {
                int index = r.Next(CityIdCityName.Count());
                addresses.Add(new Address(CityIdCityName[index].cityName, CityIdCityName[index].cityId, StreetNames[r.Next(StreetNames.Count())], GenerateHouseNumber()));
                n++;
            }
            return addresses;
        }
        private string GenerateHouseNumber()
        {
            int nr = r.Next(1, MaxHousenumber + 1);
            if (r.Next(101) <= PercentLetter) { return $"{nr}{(char)r.Next('a', 'z' + 1)}"; }

            return $"{nr}";
        }
    }
}
