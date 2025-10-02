using System.Collections.Generic;

namespace BedrijvenDomein
{
    public class Adres
    {
        public Adres(string woonplaats, int postcode, string straatnaam, string huisnummer, List<string> errors)
        {
            var errorAdres = new List<string>();

            try { Woonplaats = woonplaats; } catch (BedrijfException ex) { errorAdres.Add(ex.Message); }
            try { Straatnaam = straatnaam; } catch (BedrijfException ex) { errorAdres.Add(ex.Message); }
            try { Huisnummer = huisnummer; } catch (BedrijfException ex) { errorAdres.Add(ex.Message); }
            try { Postcode = postcode; } catch (BedrijfException ex) { errorAdres.Add(ex.Message); }

            if (errorAdres.Count > 0)
            {
                errorAdres.Insert(0, "--->Fout bij inlezen van adres<---");
                errorAdres.Add(" ");
                errors.AddRange(errorAdres);
                
            }
        }

        private string woonplaats;
        public string Woonplaats
        {
            get { return woonplaats; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Length >= 2) woonplaats = value;
                else throw new BedrijfException ("'woonplaats' heeft minder dan 2 karakters");
            }
        }

        private string straatnaam;
        public string Straatnaam
        {
            get { return straatnaam; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) straatnaam = value;
                else throw new BedrijfException("'straatnaam' is vereist");
            }
        }

        private string huisnummer;
        public string Huisnummer
        {
            get { return huisnummer; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && char.IsDigit(value.Trim()[0])) huisnummer = value;
                else throw new BedrijfException("'huisnummer' vereist");
            }

        }

        private int postcode;

        public int Postcode
        {
            get { return postcode; }
            set
            {
                if (value >= 1000 && value <= 9999) postcode = value;
                else throw new BedrijfException("'postcode' ligt niet binnen bereik van 1000 - 9999");
            }
        }
            public override string ToString()
        {
            return $"{Woonplaats}, {Postcode}, {Straatnaam}, {Huisnummer}";
        }
    }
}
