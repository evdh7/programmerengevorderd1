using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.DTOs
{
    public class LastNameDTO
    {
        public LastNameDTO(string lastName, int? frequency, string country)
        {
            LastName = lastName;
            Frequency = frequency;
            Country = country;
        }

        public string LastName { get; set; }
        public int? Frequency { get; set; }  // optional
        public string Country { get; set; }
    }
}
