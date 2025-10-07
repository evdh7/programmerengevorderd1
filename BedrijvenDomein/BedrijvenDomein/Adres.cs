namespace BedrijvenDomein
{
    public class Adres
    {
        public List<string> Errors { get; set; } = new();
        public Adres(string woonplaats, int postcode, string straatnaam, string huisnummer)
        {
            Woonplaats = woonplaats;
            Straatnaam = straatnaam;
            Huisnummer = huisnummer;
            Postcode = postcode;

            if (Errors.Count > 0)
            {
                Errors.Insert(0, "--->Fout bij inlezen van adres<---");
                Errors.Add(" ");
            }

        }

        public override string ToString()
        {
            return $"{Woonplaats}, {Postcode}, {Straatnaam}, {Huisnummer}";
        }

        private string woonplaats;
        public string Woonplaats
        {
            get { return woonplaats; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Length >= 2) woonplaats = value;
                else Errors.Add("'woonplaats' heeft minder dan 2 karakters");
            }
        }

        private string straatnaam;
        public string Straatnaam
        {
            get { return straatnaam; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) straatnaam = value;
                else Errors.Add("'straatnaam' is vereist");
            }
        }

        private string huisnummer;
        public string Huisnummer
        {
            get { return huisnummer; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && char.IsDigit(value.Trim()[0])) huisnummer = value;
                else Errors.Add("'huisnummer' vereist");
            }

        }

        private int postcode;

        public int Postcode
        {
            get { return postcode; }
            set
            {
                if (value >= 1000 && value <= 9999) postcode = value;
                else Errors.Add("'postcode' ligt niet binnen bereik van 1000 - 9999");
            }
        }


    }

}




