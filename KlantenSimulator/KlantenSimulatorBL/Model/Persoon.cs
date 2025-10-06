using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Model
{
    public class Persoon
    {
        public Persoon(int id, string voornaam, string familienaam, int geboortedatum, Adres adres)
        {
            Id = id;
            Voornaam = voornaam;
            Familienaam = familienaam;
            Geboortedatum = geboortedatum;
            Adres = adres;
        }

        public int Id { get; set; }
        public string Voornaam { get; set; }
        public string Familienaam { get; set; }
        public int Geboortedatum { get; set; }
        public Adres Adres { get; set; }


    }
}
