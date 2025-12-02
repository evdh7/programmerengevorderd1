using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vakantiepark2DL.TO
{
    public class VakantieparkTO
    {
        public VakantieparkTO(int? id, string naam, string locatie, int contactpersoonId)
        {
            Id = id;
            Naam = naam;
            Locatie = locatie;
            ContactpersoonID = contactpersoonId;
        }
        public int? Id { get; set; }
        public string Naam { get; set; }
        public string Locatie { get; set; }
        public int ContactpersoonID { get; set; }
    }
}
