using KlantenSimulatorBL.Model;

namespace KlantenSimulatorBL.Beheer
{
    public class AdresSimulator
    {
        private Random r = new Random();
        private List<string> straatnamen = new();
        private List<(int postcode, string gemeente)> postcodeGemeente = new();
        private int maxHuisnummer;
        private int percentLetter;

        public AdresSimulator(List<string> Straatnamen, List<(int postcode, string gemeente)> PostcodeGemeente, int MaxHuisnummer, int PercentLetter)
        {
            Straatnamen = straatnamen;
            PostcodeGemeente = postcodeGemeente;
            MaxHuisnummer = maxHuisnummer;
            PercentLetter = percentLetter;
        }
        public List<Adres> GeefAdressen(int aantal)
        {
            List<Adres> adressen = new();
            int n = 0;
            while (n < aantal)
            {
                int index = r.Next(postcodeGemeente.Count());
                adressen.Add(new Adres(postcodeGemeente[index].gemeente, postcodeGemeente[index].postcode, straatnamen[r.Next(straatnamen.Count())], GenereerHuisnummer()));
                n++;
            }
            return adressen;
        }
        private string GenereerHuisnummer()
        {
            int nr = r.Next(1, maxHuisnummer + 1);
            if (r.Next(101) <= percentLetter) { return $"{nr}{(char)r.Next('a', 'z' + 1)}"; }

            return$"{nr}";
            //return (r.Next(101) <= percentLetter) ? $"{nr}{(char)('a' + r.Next(0, 26))}";
        }
    }
}
