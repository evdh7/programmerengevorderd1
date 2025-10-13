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

        List<Cursus> cursussen = new List<Cursus>();

        public int? Id { get; set; }
        public string Naam { get; set; }
        public int V1 { get; }
        public string V2 { get; }
    }
}
