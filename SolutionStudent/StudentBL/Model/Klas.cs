using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentBL.Model
{
    public class Klas
    {
        public Klas(string klasnaam)
        {
            Naam = klasnaam;

        }
        public Klas(int id, string lokaal)
        {
            Id = id;
            Lokaal = lokaal;
        }

        public int? Id { get; set; }
        public string Naam { get; set; }
        public string Lokaal { get; set; }
    }
}
