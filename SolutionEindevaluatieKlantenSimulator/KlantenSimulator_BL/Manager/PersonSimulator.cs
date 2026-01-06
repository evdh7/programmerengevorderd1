using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KlantenSimulatorBL.DTOs.NameDTO;

namespace KlantenSimulatorBL.Manager
{
    public class PersonSimulator
    {
        private Random r = new Random();
        private readonly List<NameEntry> firstNames = [];
        private readonly List<NameEntry> lastNames = [];
        private List<Address> addresses = [];
        private readonly int minAge;
        private readonly int maxAge;

        public PersonSimulator(List<NameEntry> firstNames, List<NameEntry> lastNames, List<Address> addresses, int minAge, int maxAge)
        {

            this.firstNames = firstNames;
            this.lastNames = lastNames;
            this.addresses = addresses;
            this.minAge = minAge;
            this.maxAge = maxAge;
        }

        public List<Person> MakePerson(int amount)
        {
            var people = new List<Person>();
            int personsMade = 0;
            int id = 0;
            while (personsMade < amount)
            {                
                id++;
                NameEntry firstName = PickWeightedRandom(firstNames);
                NameEntry lastName = PickWeightedRandom(lastNames);
                Address address = addresses[r.Next(addresses.Count)];

                Person person = new(id, firstName.Name, lastName.Name, MakeDateOfBirth(minAge, maxAge), address);

                if (!people.Contains(person))
                {
                    personsMade++;
                    people.Add(person);
                }

            }
            return people.ToList();
        }       

        private DateTime MakeDateOfBirth(int minLeeftijd, int maxLeeftijd)
        {
            DateTime now = DateTime.Now;
            DateTime min = now.AddYears(-minLeeftijd);
            DateTime max = now.AddYears(-maxLeeftijd);
            TimeSpan span = min - max;
            double range = span.TotalSeconds;
            return max.AddSeconds(r.NextDouble() * range);
        }
        private NameEntry PickWeightedRandom(List<NameEntry> entries)
        { 
            int max = entries[^1].CumulativeWeight; // last row has max cumulative
            int roll = r.Next(1, max + 1); // roll a number between 0 and the max cumulative

            foreach (var e in entries)
            {
                if (roll <= e.CumulativeWeight)
                    return e;
            }

            return entries[^1];
        }

    }
}

    
