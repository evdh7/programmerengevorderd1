using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentBL.Model
{
    public class Cursus
    {
        public Cursus(string naam)
        {
            Naam = naam;
        }

        public Cursus(int id, string naam)
        {
            Id = id;
            Naam = naam;
        }

        public int? Id { get; set; }
        public string Naam { get; set; }
      
    }
}
