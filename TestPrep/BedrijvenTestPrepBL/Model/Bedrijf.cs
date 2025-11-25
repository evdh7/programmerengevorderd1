namespace BedrijvenTestPrepBL.Model
{
    public class Bedrijf
    {
        private readonly List<string> errors = new();
        public IReadOnlyList<string> Errors => errors;
        public Bedrijf(string name, string industrie, string sector, string location, int year, string info, List<Persoon> personeelsleden)
        {
            Name = name;
            Industrie = industrie;
            Sector = sector;
            Location = location;
            Year = year;
            Info = info;
            ValidatePersoneel(personeelsleden);

            if (Errors.Count > 0)
            {
                errors.Insert(0, "--->Fout bij het inlezen van bedrijf<---");
                errors.Add(" ");
            }
        }

        private string name;
        public string Name 
        {
            get { return name; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) name = value;
                else errors.Add("'bedrijfsnaam' is vereist.");

            }
        }

        private string industrie;

        public string Industrie
        {
            get { return industrie; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) industrie = value;
                else errors.Add("'industrie' is vereist.");
            }
        }

        private string sector;

        public string Sector 
        {
            get { return sector; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) sector = value;
                else errors.Add("'sector' is vereist.");

            }
        }
        public string Location { get; set; }
        private int year;
        public int Year
        {
            get { return year; }
            set
            {
                if (value == 0) errors.Add("'jaar' is vereist");
                else if (value <= DateTime.Now.Year) year = value;
                else errors.Add("'jaar' mag niet in de toekomst liggen");
            }
        }

        private string info;
        public string Info
        {
            get { return info; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) info = value;
                else errors.Add("'extra' is vereist.");
            }
        }

        private readonly List<Persoon> personeel = new();
        public IReadOnlyList<Persoon> Personeel => personeel;

        private void ValidatePersoneel(List<Persoon> personeelsleden)
        {
            if (personeelsleden == null || personeelsleden.Count == 0)
            {
                errors.Add("Een bedrijf moet minstens 1 personeelslid hebben");
            }
            else
            {
                foreach (var p in personeelsleden)
                {
                    VoegPersoneelToe(p);
                }
            }

        }

        public void VoegPersoneelToe(Persoon p)
        {
            bool bestaatAl = personeel.Any(existing => existing.Id == p.Id || (existing.FirstName == p.FirstName && existing.LastName == p.LastName && existing.DateOfBirth == p.DateOfBirth));

            if (p == null)
            {
                errors.Add("Persoon mag niet null zijn");
                return;
            }

            else if (bestaatAl)
            {
                errors.Add("'personeelslid' bestaat al");

            }

            else
            {
                personeel.Add(p);
                errors.AddRange(p.Errors);
            }

        }
    }
}
