using KlantenSimulatorBL.DTOs;
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
        private readonly Random r = new();

        public CountryDTO Country;
        public List<CityDTO> Cities {  get; }
        public List<string>? Streets { get; }
        public bool HasStreetCityLink { get; }
        public int MaxHousenumber { get; }
        public int? PercentLetter { get; }

        public AddressSimulator(CountryDTO country, bool hasStreetCityLink, int maxHousenumber, int? percentLetter)
        {
            Country = country;
            Cities = country.Cities;
            HasStreetCityLink = hasStreetCityLink;
            MaxHousenumber = maxHousenumber;
            PercentLetter = percentLetter;

            if (country.Addresses.Count>0) 
            {
                Streets = [.. country.Addresses];
            }
        }
        public List<Address> GetAddresses(int amount)
        {
            List<Address> addresses = new();

            for (int i = 0; i < amount; i++)
            {
                if (HasStreetCityLink)
                {
                    var city = Cities[r.Next(Cities.Count)];

                    var street = city.Addresses.ElementAt(r.Next(city.Addresses.Count));

                    addresses.Add(new Address(city.Name, city.Id, street, GenerateHouseNumber()));
                }

                else
                {
                    var city = Cities[r.Next(Cities.Count)];

                    var street = Streets[r.Next(Streets.Count)];

                    addresses.Add(new Address(city.Name, city.Id, street, GenerateHouseNumber()));
                }

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
