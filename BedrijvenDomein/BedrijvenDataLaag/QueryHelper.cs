using BedrijvenDomein;

namespace BedrijvenDataLaag
{

    public class QueryHelper
    {
        private readonly BedrijvenDataBeheerder beheerder;

        public QueryHelper(BedrijvenDataBeheerder beheerder)
        {
            this.beheerder = beheerder;
        }
        public List<string> GeefBedrijvenGeordend()
        {
            var res = beheerder.AlleBedrijven
                .Select(b=> b.Naam)
                .Distinct()
                .OrderBy(naam => naam)
                .ToList();
            return res;

        }



        public List<string> GeefBedrijvenMetJaar()
        {
            var res = beheerder.AlleBedrijven
                .OrderBy(b => b.Jaaroprichting)
                .Select(b => $"{b.Naam} - {b.Jaaroprichting}")
                .ToList();
            return res;
        }



        public List<string> GeefBedrijvenMetAantal()
        {
            var res = beheerder.AlleBedrijven
                .OrderByDescending(b => b.Personeel.Count)
                .Take(10)
                .Select(b => $"{b.Naam} - {b.Personeel.Count}")
                .ToList();

            return res;
        }

        public List<string> GeefAantalPersoneelPerGemeente()
        {
            var res = beheerder.AlleBedrijven
                .SelectMany(b=>b.Personeel)
                .GroupBy(p => p.Adres.Woonplaats)
                .Select(g => $"{g.Key} - {g.Count()} medewerkers")
                .ToList();
            return res;
        }
        
        public List<string> GeefPersoneelPerGemeente(string gemeente)
        {
            var res = beheerder.AlleBedrijven
                .SelectMany(b=>b.Personeel)
                .Where(p=> p.Adres.Woonplaats==gemeente)
                .Select(p => $"{p.Voornaam} {p.Achternaam}")
                .ToList();
            return res;

        }

        public List<string> GeefSectorenAantalBedrijven()
        {
            var res = beheerder.AlleBedrijven
                .GroupBy(b => b.Sector)
                .Select(g => $" {g.Key} - {g.Count()} bedrijven")
                .ToList();
                

            return res;
        }

        public List <string> GeefNamenBedrijvenPerIndustrie()
        {
            var res = beheerder.AlleBedrijven
                .GroupBy(b => b.Industrie)
                .Select(g=>$"{g.Key}: {string.Join(", ", g.Select(b=>b.Naam))}")
                .ToList();

            return res;
        }
    }
}
