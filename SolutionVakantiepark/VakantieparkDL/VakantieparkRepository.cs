using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VakantieparkBL;
using VakantieparkBL.Interfaces;
using VakantieparkBL.Model;

namespace VakantieparkDL
{
    public class VakantieparkRepository : IVakantieparkRepository
    {
        class MapFromDomain()
        {
            internal static VakantieparkDTO MapVakantiepark(Vakantiepark v)
            {
                return new VakantieparkDTO(v.Id,v.Naam,v.Locatie,v.Capaciteit(),v.MaxCapaciteit(),v.Wooneenheden.Count,v.Faciliteiten.Count,v.Contactpersoon.Email);
            }
        }
        private int vakantieparkId = 1;
        private int wooneenheidId = 1;
        private int faciliteitenId = 1;
        private int contactpersoonId = 1;

        private Dictionary<int, Faciliteit> faciliteiten = new();
        private Dictionary<int,Vakantiepark> vakantieparken = new();
        private Dictionary<int,Wooneenheid> wooneenheden = new();
        private Dictionary<int,Contactpersoon> contactpersonen = new();

        public VakantieparkRepository()
        {
            contactpersonen.Add(contactpersoonId, new Contactpersoon(contactpersoonId, "Jos", "jos@gmail", "09214568")); contactpersoonId++;
            contactpersonen.Add(contactpersoonId, new Contactpersoon(contactpersoonId, "Julie", "julie@gmail", "09214567")); contactpersoonId++;
            contactpersonen.Add(contactpersoonId, new Contactpersoon(contactpersoonId, "Inga", "inga@gmail", "092145689")); contactpersoonId++;
            faciliteiten.Add(faciliteitenId, new Faciliteit(faciliteitenId, "Speeltuin"));faciliteitenId++;
            faciliteiten.Add(faciliteitenId, new Faciliteit(faciliteitenId, "Zwembad")); faciliteitenId++;
            faciliteiten.Add(faciliteitenId, new Faciliteit(faciliteitenId, "Tennis")); faciliteitenId++;
            faciliteiten.Add(faciliteitenId, new Faciliteit(faciliteitenId, "Sauna")); faciliteitenId++;
            faciliteiten.Add(faciliteitenId, new Faciliteit(faciliteitenId, "Fietsverhuur")); faciliteitenId++;
            faciliteiten.Add(faciliteitenId, new Faciliteit(faciliteitenId, "Bowling")); faciliteitenId++;
            wooneenheden.Add(wooneenheidId, new Wooneenheid(wooneenheidId, 4, "bosstraat 1", HuisStatus.InGebruik)); wooneenheidId++;
            wooneenheden.Add(wooneenheidId, new Wooneenheid(wooneenheidId, 4, "bosstraat 2", HuisStatus.InGebruik)); wooneenheidId++;
            wooneenheden.Add(wooneenheidId, new Wooneenheid(wooneenheidId, 6, "bosstraat 7", HuisStatus.InGebruik)); wooneenheidId++;
            wooneenheden.Add(wooneenheidId, new Wooneenheid(wooneenheidId, 8, "bosstraat 18", HuisStatus.InGebruik)); wooneenheidId++;
            wooneenheden.Add(wooneenheidId, new Wooneenheid(wooneenheidId, 6, "bosstraat 14", HuisStatus.InGebruik)); wooneenheidId++;
            wooneenheden.Add(wooneenheidId, new Wooneenheid(wooneenheidId, 4, "speelstraat 1", HuisStatus.InGebruik)); wooneenheidId++;
            wooneenheden.Add(wooneenheidId, new Wooneenheid(wooneenheidId, 12, "speelstraat 11", HuisStatus.InHerstel)); wooneenheidId++;
            wooneenheden.Add(wooneenheidId, new Wooneenheid(wooneenheidId, 20, "speelstraat 8", HuisStatus.InGebruik)); wooneenheidId++;
            wooneenheden.Add(wooneenheidId, new Wooneenheid(wooneenheidId, 3, "speelstraat 19", HuisStatus.InGebruik)); wooneenheidId++;
            wooneenheden.Add(wooneenheidId, new Wooneenheid(wooneenheidId, 2, "genietdreef 1", HuisStatus.InGebruik)); wooneenheidId++;
            vakantieparken.Add(vakantieparkId, new Vakantiepark(vakantieparkId, "Ossemeersen", "Gent",new List<Faciliteit>() { faciliteiten[1], faciliteiten[1], faciliteiten[2], faciliteiten[5] }, wooneenheden.Values.Where(x => x.Adres.StartsWith("bos")).ToList(), contactpersonen[1])); vakantieparkId++;
            vakantieparken.Add(vakantieparkId, new Vakantiepark(vakantieparkId, "Wandelaar", "Geel", new List<Faciliteit>() { faciliteiten[6], faciliteiten[1], faciliteiten[2], faciliteiten[5] }, wooneenheden.Values.Where(x => x.Adres.StartsWith("speel")).ToList(), contactpersonen[2])); vakantieparkId++;
            vakantieparken.Add(vakantieparkId, new Vakantiepark(vakantieparkId, "De Genieter", "Aalst", new List<Faciliteit>() { faciliteiten[2], faciliteiten[1], faciliteiten[4], faciliteiten[5], faciliteiten[6] }, wooneenheden.Values.Where(x => !x.Adres.StartsWith("bos")).ToList(), contactpersonen[1])); vakantieparkId++;
        }

        public List<VakantieparkDTO> GeefVakantieparken()
        {
            return vakantieparken.Values.Select(x=>MapFromDomain.MapVakantiepark(x)).ToList();
        }
        public List<Contactpersoon> GeefContacten()
        {
            return contactpersonen.Values.ToList();
        }
        public List<Faciliteit> GeefFaciliteiten()
        {
            return faciliteiten.Values.ToList();
        }

        public void VoegFaciliteitToe(Faciliteit faciliteit)
        {
            faciliteit.Id = faciliteitenId;
            faciliteiten.Add(faciliteitenId,faciliteit);
            faciliteitenId++;
        }

        public List<Wooneenheid> GeefWooneenheden(int vakantieparkID)
        {
            return vakantieparken[vakantieparkID].Wooneenheden.ToList();
        }

        public void VoegVakantieparkToe()
        {

        }
    }
}
