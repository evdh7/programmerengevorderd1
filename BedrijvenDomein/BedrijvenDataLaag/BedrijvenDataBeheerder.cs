using BedrijvenDomein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrijvenDataLaag
{
    public class BedrijvenDataBeheerder
    {
        private Dictionary<string, Bedrijf> bedrijven = new(); //sleutel bedrijfsnaam
        private Dictionary<string, List <Personeel>> personeel = new(); //sleutel gemeente
        public BedrijvenDataBeheerder(List<Bedrijf> bedrijvenList) 
        {
            foreach(var b in bedrijvenList)
            {
                bedrijven.Add(b.Naam, b);
                foreach (var p in b.Personeel)
                {
                    if (!personeel.ContainsKey(p.Adres.Woonplaats)) 
                        personeel.Add(p.Adres.Woonplaats, new List<Personeel>());
                    personeel[p.Adres.Woonplaats].Add(p);
                }
            }


        }
        public IReadOnlyList<Personeel> GeefPersoneelBedrijf(string bedrijfsnaam)
        {
            return bedrijven[bedrijfsnaam].Personeel;

        }
        public List<Personeel> GeefPersoneelGemeente(string gemeente)
        {
            return personeel[gemeente];

        }


    }
}
