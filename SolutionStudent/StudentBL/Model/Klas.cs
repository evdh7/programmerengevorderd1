using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentBL.Model
{
    public class Klas
    {
        public Klas(string naam)
        {
            Naam = naam;

        }
        public Klas(int id, string naam)
        {
            Id = id;
            Naam = naam;
        }

        public int? Id { get; set; }
        public string Naam { get; set; }
        public string? Lokaal { get; set; }
    }
}
