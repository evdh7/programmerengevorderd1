using KlantenSimulatorBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Manager
{
    public class PersonSimulator
    {
        private Random r = new Random();
        private List<string> firstNames = new();
        private List<string> lastNames = new();
        private List<Address> addresses = new();
        private int minAge, maxAge;

        public PersonSimulator(List<string> firstNames, List<string> lastNames, List<Address> addresses, int minAge, int maxAge)
        {
            this.firstNames = firstNames;
            this.lastNames = lastNames;
            this.addresses = addresses;
            this.minAge = minAge;
            this.maxAge = maxAge;
           
        }

        public List<Person> MakePerson(int amount)
        {
            HashSet<Person> data = new();
            int personsMade = 0;
            int id = 0;
            while (personsMade < amount)
            {
                string firstName = this.firstNames[r.Next(firstNames.Count())];
                string lastName = this.lastNames[r.Next(lastNames.Count())];

                Person persoon = new(id, firstName, lastName, MakeDateOfBirth(minAge, maxAge), addresses[r.Next(addresses.Count())]);
                if (!data.Contains(persoon))
                {
                    personsMade++;
                    data.Add(persoon);
                }

                data.Add(persoon);

            }
            return data.ToList();
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

    }
}

    
