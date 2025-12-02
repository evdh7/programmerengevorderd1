using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.DTOs
{
    public class AddressDTO
    {

        public string Country;
        public string City;
        public string Street;

        public AddressDTO(string country, string city, string street)
        {
            Country = country;
            City = city;
            Street = street;
        }
    }
}
