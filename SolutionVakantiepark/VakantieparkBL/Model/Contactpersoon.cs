using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VakantieparkBL.Model
{
    public class Contactpersoon
    {
        public Contactpersoon(string naam, string email, string telefoon)
        {
            Naam = naam;
            Email = email;
            Telefoon = telefoon;
        }

        public Contactpersoon(int id, string naam, string email, string telefoon)
        {
            Id = id;
            Naam = naam;
            Email = email;
            Telefoon = telefoon;
        }

        public int Id { get; set; }
        public string Naam { get; set; }
        public string Email { get; set; }
        public string Telefoon { get; set; }
        public override string ToString()
        {
            return $"{Naam}-{Email}-{Telefoon}";
        }
    }
}
