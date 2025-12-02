using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Model
{
    public class Address
    {
        public Address(string country, string city, string zipcode, string streetname, int number)
        {
            Country = country;
            City = city;
            Zipcode = zipcode;
            Streetname = streetname;
            Number = number;
        }

        public string Country {  get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Streetname { get; set; }
        public int Number { get; set; }

        public override string ToString()
        {
            return $"{Country}, {City}, {Zipcode}, {Streetname}, {Number}";
        }
    }
}
