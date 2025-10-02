using BedrijvenDomein;

namespace BedrijvenDomein
{
    public class Bedrijf
    {

        public Bedrijf(string naam, string industrie, string sector, string hoofdkwartier, int jaaroprichting, string extra, List<Personeel> personeel, List<string> errors)
        {
            var errorBedrijf = new List<string>();

            try { Naam = naam; } catch (BedrijfException ex) { errorBedrijf.Add(ex.Message); }
            try { Sector = sector; } catch (BedrijfException ex) { errorBedrijf.Add(ex.Message); }
            try { Industrie = industrie; } catch (BedrijfException ex) { errorBedrijf.Add(ex.Message); }
            try { Extra = extra; } catch (BedrijfException ex) { errorBedrijf.Add(ex.Message); }
            try { Hoofdkwartier = hoofdkwartier; } catch (BedrijfException ex) { errorBedrijf.Add(ex.Message); }
            try { Jaaroprichting = jaaroprichting; } catch (BedrijfException ex) { errorBedrijf.Add(ex.Message); }


            if (personeel == null || personeel.Count == 0)
            {
                errorBedrijf.Add("Een bedrijf moet minstens 1 personeelslid hebben");
            }
            else
            {
                try { this.personeel.AddRange(personeel); } catch (BedrijfException ex) { errorBedrijf.Add(ex.Message); }

                if (errorBedrijf.Count > 0)
                {
                    errorBedrijf.Insert(0, "--->Fout bij het inlezen van bedrijf<---");
                    errorBedrijf.Add(" ");
                    errors.AddRange(errorBedrijf);
                }
            }
        }



        private string naam;
        public string Naam
        {
            get { return naam; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) naam = value;
                else throw new BedrijfException("'bedrijfsnaam' is vereist.");

            }
        }

        private string sector;
        public string Sector
        {
            get { return sector; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) sector = value;
                else throw new BedrijfException("'sector' is vereist.");

            }
        }

        private string industrie;
        public string Industrie
        {
            get { return industrie; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) industrie = value;
                else throw new BedrijfException("'industrie' is vereist.");

            }
        }

        private string extra;
        public string Extra
        {
            get { return extra; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) extra = value;
                else throw new BedrijfException("'extra' is vereist.");

            }
        }

        private string hoofdkwartier;
        public string Hoofdkwartier
        {
            get { return hoofdkwartier; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) hoofdkwartier = value;
                else throw new BedrijfException("'hoofdkwartier' is vereist.");
            }
        }

        private int jaaroprichting;
        public int Jaaroprichting
        {
            get { return jaaroprichting; }
            set
            {
                if (value <= DateTime.Now.Year && value > 0) jaaroprichting = value;
                else if (value == 0) throw new BedrijfException("'jaar' is vereist");
                else throw new BedrijfException("'jaar' mag niet in de toekomst liggen");
            }
        }

        private readonly List<Personeel> personeel = new();
        public IReadOnlyList<Personeel> Personeel => personeel;

        public void VoegPersoneelToe(Personeel nieuweLijn)
        {
            if (nieuweLijn == null) throw new BedrijfException("'personeelslid' mag niet null zijn");

            if (personeel.Any(p => p.ID == nieuweLijn.ID || (p.Voornaam == nieuweLijn.Voornaam && p.Achternaam == nieuweLijn.Achternaam && p.DateOfBirth == nieuweLijn.DateOfBirth)))
            {
                throw new BedrijfException("'personeelslid' bestaat al");
            }

            personeel.Add(nieuweLijn);
        }
    }
}
