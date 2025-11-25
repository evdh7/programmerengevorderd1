namespace BedrijvenTestPrepBL.Model
{
    public class Adres
    {
        public List<string> Errors { get; set; } = new();
        public Adres(string gemeente, int postcode, string straat, string huisnummer)
        {
            Gemeente = gemeente;
            Postcode = postcode;
            Straat = straat;
            Huisnummer = huisnummer;

            if (Errors.Count > 0)
            {
                Errors.Insert(0, "--->Fout bij inlezen van adres<---");
                Errors.Add(" ");
            }
        }

        private string gemeente;
        public string Gemeente
        {
            get { return gemeente; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Length >= 2)
                {
                    gemeente = value;
                }
                else
                {
                    Errors.Add("'gemeente' heeft minder dan 2 karakters");
                }
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
        private string straat;
        public string Straat
        {
            get { return straat; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) straat = value;
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
                else Errors.Add("'huisnummer'vereist");

            }
        }
    }
}
