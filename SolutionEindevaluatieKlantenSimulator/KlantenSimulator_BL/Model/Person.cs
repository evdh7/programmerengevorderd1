using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Model
{
    public class Person(int simulationId, string firstName, string lastName, DateTime dateOfBirth, Address address)
    {
        public int SimulationId { get; set; } = simulationId;
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public DateTime DateOfBirth { get; set; } = dateOfBirth;
        public Address Address { get; set; } = address;
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                int age = today.Year - DateOfBirth.Year;
                if (DateOfBirth.Date > today.AddYears(-age)) age--;
                return age;
            }
        }
        public override string ToString()
        {
            return $"{SimulationId}, {FirstName}, {LastName}, {DateOfBirth}, {Address}";
        }
    }
}
