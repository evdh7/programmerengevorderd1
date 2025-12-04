using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.DTOs
{
    public class LastNameDTO
    {
        public LastNameDTO(string lastName, int? frequency)
        {
            LastName = lastName;
            Frequency = frequency;
        }

        public string LastName { get; set; }
        public int? Frequency { get; set; }  // optional
    }
}
