using KlantenSimulatorBL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Model
{
    public class Name(string firstOrLastName, int frequency, Gender gender)
    {
        public string FirstOrLastName { get; set; } = firstOrLastName;
        public int Frequency { get; set; } = frequency;
        public Gender Gender { get; set; } = gender;


    }
}
