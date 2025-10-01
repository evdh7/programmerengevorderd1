using System.ComponentModel;
using System.ComponentModel.Design;

namespace BedrijvenDomein
{
    public class Personeel
    {
        public Personeel(int id, string voornaam, string achternaam, DateTime birthdate, Adres adres, string email, List<string> errors)
        {
            int i = 0;


            try { ID = id; } catch (BedrijfException ex) { errors.Add(ex.Message); i++; }
            try { Voornaam = voornaam; } catch (BedrijfException ex) { errors.Add(ex.Message); i++; }
            try { Achternaam = achternaam; } catch (BedrijfException ex) { errors.Add(ex.Message); i++; }
            try { DateOfBirth = birthdate; } catch (BedrijfException ex) { errors.Add(ex.Message); i++; }
            try { Email = email; } catch (BedrijfException ex) { errors.Add(ex.Message); i++; }
            try { Adres = adres; } catch (BedrijfException ex) { errors.Add(ex.Message); i++; }


            if (i > 0)
            {
                BedrijfException ex = new BedrijfException("-> personeel bevat fouten");
                errors.Insert(errors.Count - i, ex.Message);
            }

        }

        private int id;
        public int ID
        {
            get { return id; }
            set
            {
                if (value > 0) { id = value; }
                else throw new BedrijfException("ID nummer moet groter zijn dan 0");
            }
        }

        private string voornaam;
        public string Voornaam
        {
            get { return voornaam; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) { voornaam = value; }
                else throw new BedrijfException("'voornaam' is vereist");
            }
        }

        private string achternaam;
        public string Achternaam 
        {
            get { return achternaam; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) { achternaam = value; }
                else throw new BedrijfException("'achternaam' is vereist");
            }
        }

        private DateTime dateofbirth;
        public DateTime DateOfBirth
        {
            get { return dateofbirth; }
            set
            {
                if (value < DateTime.Today.AddYears(-18) || value==DateTime.MinValue) { dateofbirth = value; }
                else throw new BedrijfException("personeelslid moet minstens 18 jaar zijn en geboortedatum mag niet leeg zijn");


            }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) { email = value; }
                else throw new BedrijfException("email is niet geldig");
            }
        }

        //TO DO email en checken string voor en na @
        //TO DO overal check of het niet null is

        private Adres adres;
        
        public Adres Adres
        {
            get { return adres; }
            set
            {
                if (value == null)
                    throw new BedrijfException("adres is null");

                else adres = value;
            }
        }
        public override string ToString()
        {
            return $"{Voornaam}, {Achternaam}, {Adres}, {Email} ";
        }
    }
}
