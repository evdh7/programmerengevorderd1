using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VakantieparkBL.DTOs
{

    public class VakantieparkDTO
    {
        public VakantieparkDTO(int id, string naam, string locatie, int capaciteit, int aantalWooneenheden, int aantalFaciliteiten, string contactEmail)
        {
            Id = id;
            Naam = naam;
            Locatie = locatie;
            Capaciteit = capaciteit;
            AantalWooneenheden = aantalWooneenheden;
            AantalFaciliteiten = aantalFaciliteiten;
            ContactEmail = contactEmail;
        }

        public int Id {  get; set; }
        public string Naam { get; set; }
        public string Locatie { get; set; }
        public int Capaciteit { get; set; }
        public int AantalWooneenheden { get; set; }
        public int AantalFaciliteiten { get; set; }
        public string ContactEmail { get; set; }
    }
}
