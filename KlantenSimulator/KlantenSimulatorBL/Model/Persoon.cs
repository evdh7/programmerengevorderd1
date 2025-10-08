using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Model
{
    public class Persoon
    {
        public Persoon(int id, string voornaam, string familienaam, string email, DateTime geboortedatum, Adres adres)
        {
            Id = id;
            Voornaam = voornaam;
            Familienaam = familienaam;
            Email = email; 
            Geboortedatum = geboortedatum;
            Adres = adres;
        }

        public int Id { get; set; }
        public string Voornaam { get; set; }
        public string Familienaam { get; set; }
        public string Email { get; set; }
        public DateTime Geboortedatum { get; set; }
        public Adres Adres { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Persoon persoon &&
                   (Id == persoon.Id ||
                   Email == persoon.Email);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Email);
        }
        public override string ToString()
        {
            return $"{Id}, {Voornaam}, {Familienaam}, {Email}, {Geboortedatum}, {Adres}";
        }
    }
}
