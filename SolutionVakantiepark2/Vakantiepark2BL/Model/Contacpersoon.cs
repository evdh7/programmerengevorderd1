using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vakantiepark2BL.Model
{
    public class Contactpersoon
    {
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

    }
}
