using System.Collections.Immutable;

namespace BedrijvenDomein
{
    public class Bedrijf
    {

        public Bedrijf(string naam, string industrie, string sector, string hoofdkwartier, int jaaroprichting, string extra, List<Personeel> personeel,List<string> errors)
        {
            int i = 0;

            try { Naam = naam; } catch (BedrijfException ex) { errors.Add(ex.Message); i++; }
            try { Sector = sector; } catch (BedrijfException ex) { errors.Add(ex.Message); i++; }
            try { Industrie = industrie; } catch (BedrijfException ex) { errors.Add(ex.Message); i++; }
            try { Extra = extra; } catch (BedrijfException ex) { errors.Add(ex.Message); i++; }
            try { Hoofdkwartier = hoofdkwartier; } catch (BedrijfException ex) { errors.Add(ex.Message); i++; }
            try { Jaaroprichting = jaaroprichting; } catch (BedrijfException ex) { errors.Add(ex.Message); i++; }
            try { Personeel = personeel; } catch (BedrijfException ex) { errors.Add(ex.Message); i++; }

            if (i > 0)
            {
                BedrijfException ex = new BedrijfException("-> bedrijf bevat fouten");
                errors.Insert(errors.Count - i, ex.Message);
            }
        }


        private string naam;
        public string Naam
        {
            get { return naam; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) naam = value;
                else throw new BedrijfException("Bedrijfsnaam is vereist.");

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
                if (value <= DateTime.Now.Year && value >= 0) jaaroprichting = value;
                else throw new BedrijfException("'jaar' mag niet in de toekomst liggen");
            }
        }

        private List<Personeel> personeel;
        public List<Personeel> Personeel
        {
            get { return personeel; }
            set
            {
                if (value == null || value.Count == 0)
                {
                    throw new BedrijfException("Een bedrijf moet minstens 1 personeelslid hebben.");
                }
                personeel = value;
               
            }

        }

        //TODO check geen dubbels in Personeel een nieuw mag dus niet hetzelfde zijn als ...
    }
}
