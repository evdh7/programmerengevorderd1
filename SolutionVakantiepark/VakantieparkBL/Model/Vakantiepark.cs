using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VakantieparkBL.Exceptions;

namespace VakantieparkBL.Model
{
    public class Vakantiepark
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Locatie { get; set; }
        public Contactpersoon Contactpersoon { get; set; }
        private List<Faciliteit> faciliteiten;
        private List<Wooneenheid> wooneenheden;

        public Vakantiepark(int id, string naam, string locatie, List<Faciliteit> faciliteiten, List<Wooneenheid> wooneenheden, Contactpersoon contactpersoon)
        {
            Id = id;
            Naam = naam;
            Locatie = locatie;
            this.faciliteiten = faciliteiten;
            this.wooneenheden = wooneenheden;
            Contactpersoon = contactpersoon;
        }
        public IReadOnlyList<Faciliteit> Faciliteiten => faciliteiten;
        public IReadOnlyList<Wooneenheid> Wooneenheden => wooneenheden;
        public void VoegFaciliteitToe(Faciliteit faciliteit)
        {
            if (faciliteit == null) throw new VakantieparkException("VoegFaciliteitToe");
            if (faciliteiten.Contains(faciliteit)) throw new VakantieparkException("VoegFaciliteitToe");
            faciliteiten.Add(faciliteit);
        }
        public void VerwijderFaciliteit(Faciliteit faciliteit)
        {
            if (faciliteit == null) throw new VakantieparkException("VerwijderFaciliteit");
            if (!faciliteiten.Contains(faciliteit)) throw new VakantieparkException("VerwijderFaciliteit");
            faciliteiten.Remove(faciliteit);
        }
        public int Capaciteit() => wooneenheden.Where(x => x.Status == HuisStatus.InGebruik).Sum(x => x.Capaciteit);
        public int MaxCapaciteit() => wooneenheden.Sum(x => x.Capaciteit);
    }
}
