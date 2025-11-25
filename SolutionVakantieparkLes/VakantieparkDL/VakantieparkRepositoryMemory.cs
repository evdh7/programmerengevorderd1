using VakantieparkBL.DTOs;
using VakantieparkBL.Interfaces;
using VakantieparkBL.Model;

namespace VakantieparkDL
{
    public class VakantieparkRepositoryMemory : IVakantieparkRepository
    {
        private int vakantieparkId = 1;
        private int contactpersoonId = 1;
        private int faciliteitenId = 1;
        private int wooneenhedenId = 1;

        private Dictionary<int, Vakantiepark> vakantieparken = new();
        private Dictionary<int, Contactpersoon> contactpersonen = new();
        private Dictionary<int, Faciliteit> faciliteiten = new();
        private Dictionary<int, Wooneenheid> wooneenheden = new();

        public VakantieparkRepositoryMemory()
        {
            contactpersonen.Add(contactpersoonId, new Contactpersoon(contactpersoonId, "jos", "jos@gmail.com", "01234566"))
                ; contactpersoonId++;
            contactpersonen.Add(contactpersoonId, new Contactpersoon(contactpersoonId, "julie", "julie@gmail.com", "012345669"))
                ; contactpersoonId++;
            faciliteiten.Add(faciliteitenId, new Faciliteit(faciliteitenId, "speeltuin")); faciliteitenId++;
            faciliteiten.Add(faciliteitenId, new Faciliteit(faciliteitenId, "trampoline")); faciliteitenId++;

            faciliteiten.Add(faciliteitenId, new Faciliteit(faciliteitenId, "bowling")); faciliteitenId++;

            faciliteiten.Add(faciliteitenId, new Faciliteit(faciliteitenId, "tennis")); faciliteitenId++;

            faciliteiten.Add(faciliteitenId, new Faciliteit(faciliteitenId, "zwembad")); faciliteitenId++;

            wooneenheden.Add(wooneenhedenId, new Wooneenheid(wooneenhedenId, "hoofdstraat 71", 3, HuisStatus.Beschikbaar)); wooneenhedenId++;
            wooneenheden.Add(wooneenhedenId, new Wooneenheid(wooneenhedenId, "hoofdstraat 11", 15, HuisStatus.Beschikbaar)); wooneenhedenId++;
            wooneenheden.Add(wooneenhedenId, new Wooneenheid(wooneenhedenId, "speelstraat 1", 5, HuisStatus.Beschikbaar)); wooneenhedenId++;
            wooneenheden.Add(wooneenhedenId, new Wooneenheid(wooneenhedenId, "speelstraat 11", 2, HuisStatus.Beschikbaar)); wooneenhedenId++;
            wooneenheden.Add(wooneenhedenId, new Wooneenheid(wooneenhedenId, "speelstraat 15", 7, HuisStatus.Beschikbaar)); wooneenhedenId++;
            wooneenheden.Add(wooneenhedenId, new Wooneenheid(wooneenhedenId, "speelstraat 17", 9, HuisStatus.Beschikbaar)); wooneenhedenId++;
            vakantieparken.Add(vakantieparkId, new Vakantiepark(vakantieparkId, "Gentpark", "Gent", contactpersonen[1], new List<Faciliteit>() { faciliteiten[1], faciliteiten[2], faciliteiten[3] }, wooneenheden.Values.Where(x => x.Adres.StartsWith("hoofd")).ToList())); vakantieparkId++;
            vakantieparken.Add(vakantieparkId, new Vakantiepark(vakantieparkId, "Lokeren Meersen", "Lokeren", contactpersonen[2], new List<Faciliteit>() { faciliteiten[1], faciliteiten[2], faciliteiten[3] }, wooneenheden.Values.Where(x => x.Adres.StartsWith("speel")).ToList())); vakantieparkId++;

        }

        public List<VakantieparkDTO> GeefVakantieparken()
        {
            return vakantieparken.Values.Select(x=>VakantieparkMapper.MapUitDomein(x)).ToList();
        }

    }
}
