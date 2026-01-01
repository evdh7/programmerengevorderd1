using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Model
{
    public class Address(string city, int cityId, string street, string housenumber)
    {
        public string City { get; set; } = city;

        public int CityId { get; set; } = cityId;
        public string StreetName { get; set; } = street;

        public string HouseNumber { get; set; } = housenumber;

        public override string ToString()
        {
            return $"{City}, {StreetName}, {HouseNumber}";
        }
    }
}
