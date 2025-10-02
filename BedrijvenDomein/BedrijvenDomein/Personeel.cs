using System.ComponentModel.Design;

namespace BedrijvenDomein
{
    public class Personeel
    {
        public Personeel(int id, string voornaam, string achternaam, DateTime birthdate, Adres adres, string email, List<string> errors)
        {
            var errorPersoneel = new List<string>();

            try { ID = id; } catch (BedrijfException ex) { errorPersoneel.Add(ex.Message); }
            try { Voornaam = voornaam; } catch (BedrijfException ex) { errorPersoneel.Add(ex.Message); }
            try { Achternaam = achternaam; } catch (BedrijfException ex) { errorPersoneel.Add(ex.Message); }
            try { DateOfBirth = birthdate; } catch (BedrijfException ex) { errorPersoneel.Add(ex.Message); }
            try { Email = email; } catch (BedrijfException ex) { errorPersoneel.Add(ex.Message); }
            try { Adres = adres; } catch (BedrijfException ex) { errorPersoneel.Add(ex.Message); }


            if (errorPersoneel.Count > 0)
            {
                errorPersoneel.Insert(0, "--->Fout bij inlezen van personeel<---");
                errorPersoneel.Add(" ");
                errors.AddRange(errorPersoneel);
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
                if (value < DateTime.Today.AddYears(-18) || value == DateTime.MinValue) { dateofbirth = value; }
                else throw new BedrijfException("personeelslid moet minstens 18 jaar zijn en geboortedatum mag niet leeg zijn");


            }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new BedrijfException("email mag niet leeg zijn");

                }

                var emailGesplitst = value.Split('@'); //splits email op door @ te gebruiken als seperator

                if (emailGesplitst.Length != 2 || string.IsNullOrWhiteSpace(emailGesplitst[0]) || string.IsNullOrWhiteSpace(emailGesplitst[1]))
                {
                    throw new BedrijfException("email heeft geen geldige structuur");
                }
                else
                {
                    email = value;
                }

            }

        }

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
