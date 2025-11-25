namespace BedrijvenTestPrepBL.Model
{
    public class Persoon
    {
        public List<string> Errors { get; set; } = new();
        public Persoon(int id, string lastName, string firstName, DateTime dateOfBirth, string email, Adres adres)
        {
            Id = id;
            LastName = lastName;
            FirstName = firstName;
            DateOfBirth = dateOfBirth;
            Email = email;
            Adres = adres;

            if ((adres != null)&&(adres.Errors.Count>0))
            {
                Errors.AddRange(adres.Errors);
            }
            else if (adres==null)
            {
                Errors.Add("adres is null");
            }

            if (Errors.Count > 0)
            {
                Errors.Insert(0, "--->Fout bij inlezen van personeel<---");
                Errors.Add(" ");
            }
        }
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (value > 0) { id = value; }
                else Errors.Add("ID nummer moet groter zijn dan 0");
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) { lastName = value; }
                else Errors.Add("'achternaam' is vereist");
            }
        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) { firstName = value; }
                else Errors.Add("'achternaam' is vereist");
            }
        }
        private DateTime dateofbirth;

        public DateTime DateOfBirth
        {
            get { return dateofbirth; }
            set
            {
                if (value == DateTime.MinValue)
                {
                    Errors.Add("geboortedatum mag niet leeg zijn");
                }

                else if (value >= DateTime.Today.AddYears(-18))
                {
                    Errors.Add("personeelslid moet minstens 18 jaar zijn");

                }
                else { dateofbirth = value; }
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
                    Errors.Add("email mag niet leeg zijn");
                    return;

                }

                var emailGesplitst = value.Split('@'); //splits email op door @ te gebruiken als seperator

                if (emailGesplitst.Length != 2 ||
                    string.IsNullOrWhiteSpace(emailGesplitst[0]) ||
                    string.IsNullOrWhiteSpace(emailGesplitst[1]) ||
                    !emailGesplitst[1].Contains('.') ||
                    emailGesplitst[1].StartsWith('.') ||
                    emailGesplitst[1].EndsWith('.'))
                {
                    Errors.Add("email heeft geen geldige structuur");
                    return;
                }
                else
                {
                    email = value;
                }

            }
        }

        public Adres Adres { get; set; }

        public override string ToString()
        {
            return $"{FirstName}, {LastName}, {Adres}, {Email} ";
        }

    }
}

