using KlantenSimulatorBL.Model;

namespace KlantenSimulatorBL.Beheer
{
    public class PersoonSimulator
    {
        private Random r = new Random();
        private List<string> voornamen = new();
        private List<string> familienamen = new();
        private List<Adres> adressen = new();
        private int minLeeftijd, maxLeeftijd, minId, maxId;
        private List<string> emailExtensie = new()
        { "gmail.com", "hotmail.com", "outlook.com","yahoo.com", "telenet.be"};

        public PersoonSimulator(List<string> voornamen, List<string> familienamen, List<Adres> adressen, int minLeeftijd, int maxLeeftijd, int minId, int maxId)
        {
            this.voornamen = voornamen;
            this.familienamen = familienamen;
            this.adressen = adressen;
            this.minLeeftijd = minLeeftijd;
            this.maxLeeftijd = maxLeeftijd;
            this.minId = minId;
            this.maxId = maxId;
        }

        public List<Persoon> MaakPersoon(int aantal)
        {
            HashSet<Persoon> data = new();
            int aantalGemaakt = 0;
            while (aantalGemaakt < aantal)
            {
                string voornaam = voornamen[r.Next(voornamen.Count())];
                string familienaam = familienamen[r.Next(familienamen.Count())];

                Persoon persoon = new(r.Next(minId,maxId), voornaam, familienaam, MaakEmail(voornaam,familienaam), MaakGeboorteDatum(minLeeftijd,maxLeeftijd), adressen[r.Next(adressen.Count())]);
                if (!data.Contains(persoon))
                {
                    aantalGemaakt++;
                    data.Add(persoon);
                }
                
                data.Add( persoon );

            }
            return data.ToList();
        }
        private string MaakEmail(string voornaam, string familienaam)
        {
            return $"{voornaam}.{familienaam.Replace(" ", "").Replace("-", "")}@{emailExtensie[r.Next(emailExtensie.Count())]}";
        }
        private DateTime MaakGeboorteDatum(int minLeeftijd, int maxLeeftijd)
        {
            DateTime now = DateTime.Now;
            DateTime min = now.AddYears(-minLeeftijd);
            DateTime max = now.AddYears(-maxLeeftijd);
            TimeSpan span = min - max;
            double range = span.TotalSeconds;
            return max.AddSeconds(r.NextDouble() * range);
        }

    }
}
