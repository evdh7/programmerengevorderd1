using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Model
{
    public class Person
    {
        public Person(int id, string firstName, string lastName, DateTime dateOfBirth, Address addres)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Address = addres;
        }
        //gender?
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Address Address { get; set; }
        public override string ToString()
        {
            return $"{Id}, {FirstName}, {LastName}, {DateOfBirth}, {Address}";
        }
    }
}
