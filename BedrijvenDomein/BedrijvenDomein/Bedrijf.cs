namespace BedrijvenDomein
{
    public class Bedrijf
    {
        public List<string> Errors { get; set; } = new();

        public Bedrijf(string naam, string industrie, string sector, string hoofdkwartier, int jaaroprichting, string extra, List<Personeel> personeel)
        {
            Naam = naam;
            Industrie = industrie;
            Sector = sector;
            Hoofdkwartier = hoofdkwartier;
            Jaaroprichting = jaaroprichting;
            Extra = extra;

            if (personeel == null || personeel.Count == 0)
            {
                Errors.Add("Een bedrijf moet minstens 1 personeelslid hebben");
                Errors.Insert(0, "--->Fout bij het inlezen van bedrijf<---");

            }
            else
            {
                foreach (var p in personeel)
                {
                    VoegPersoneelToe(p);
                }
            }

            if (Errors.Count > 0)
            {
                Errors.Insert(0, "--->Fout bij het inlezen van bedrijf<---");
                Errors.Add(" ");
            }
        }

        private string naam;
        public string Naam
        {
            get { return naam; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) naam = value;
                else Errors.Add("'bedrijfsnaam' is vereist.");

            }
        }

        private string sector;
        public string Sector
        {
            get { return sector; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) sector = value;
                else Errors.Add("'sector' is vereist.");

            }
        }

        private string industrie;
        public string Industrie
        {
            get { return industrie; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) industrie = value;
                else Errors.Add("'industrie' is vereist.");

            }
        }

        private string extra;
        public string Extra
        {
            get { return extra; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) extra = value;
                else Errors.Add("'extra' is vereist.");

            }
        }

        private string hoofdkwartier;
        public string Hoofdkwartier
        {
            get { return hoofdkwartier; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) hoofdkwartier = value;
                else Errors.Add("'hoofdkwartier' is vereist.");
            }
        }

        private int jaaroprichting;
        public int Jaaroprichting
        {
            get { return jaaroprichting; }
            set
            {
                if (value == 0) Errors.Add("'jaar' is vereist");
                else if (value <= DateTime.Now.Year) jaaroprichting = value;
                else Errors.Add("'jaar' mag niet in de toekomst liggen");
            }
        }

        private readonly List<Personeel> personeel = new();
        public IReadOnlyList<Personeel> Personeel => personeel;

        public void VoegPersoneelToe(Personeel nieuweLijn)
        {
          
            bool bestaatAl = personeel.Any(p => p.ID == nieuweLijn.ID || (p.Voornaam == nieuweLijn.Voornaam && p.Achternaam == nieuweLijn.Achternaam && p.DateOfBirth == nieuweLijn.DateOfBirth));


            if (nieuweLijn == null)
            {
                Errors.Add("'personeelslid' mag niet null zijn");
                return;
            }

            else if (bestaatAl)
            {
                Errors.Add("'personeelslid' bestaat al");
                
            }
            else
            {
                personeel.Add(nieuweLijn);
                Errors.AddRange(nieuweLijn.Errors);
            }


            
        }
    }
}
