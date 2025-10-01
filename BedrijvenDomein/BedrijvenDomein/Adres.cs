namespace BedrijvenDomein
{
    public class Adres
    {
        public Adres(string woonplaats, int postcode, string straatnaam, string huisnummer, List<string> errors)
        {
            try { Woonplaats = woonplaats; } catch (BedrijfException ex) { errors.Add(ex.Message); }
            try { Straatnaam = straatnaam; } catch (BedrijfException ex) { errors.Add(ex.Message); }
            try { Huisnummer = huisnummer; } catch (BedrijfException ex) { errors.Add(ex.Message); }
            try { Postcode = postcode; } catch (BedrijfException ex) { errors.Add(ex.Message); }

            if (errors.Count > 0)
            {
                BedrijfException ex = new BedrijfException("-> adres bevat fouten");
                errors.Insert(0,ex.Message);
            }
        }

        private string woonplaats;
        public string Woonplaats
        {
            get { return woonplaats; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Length >= 2) woonplaats = value;
                else throw new BedrijfException ("woonplaats heeft minder dan 2 karakters");
            }
        }

        private string straatnaam;
        public string Straatnaam
        {
            get { return straatnaam; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) straatnaam = value;
                else throw new BedrijfException("straatnaam is vereist");
            }
        }

        private string huisnummer;
        public string Huisnummer
        {
            get { return huisnummer; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && char.IsDigit(value.Trim()[0])) huisnummer = value;
                else throw new BedrijfException("huisnummer vereist");
            }

        }

        private int postcode;

        public int Postcode
        {
            get { return postcode; }
            set
            {
                if (value >= 1000 && value <= 9999) postcode = value;
                else throw new BedrijfException("postcode ligt niet binnen bereik van 1000 - 9999");
            }
        }
            public override string ToString()
        {
            return $"{Woonplaats}, {Postcode}, {Straatnaam}, {Huisnummer}";
        }
    }
}
