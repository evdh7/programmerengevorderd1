namespace Vakantiepark2DL.TO
{
    public class ContactpersoonTO
    {
        public ContactpersoonTO(int id, string naam, string email, string telefoon)
        {
            Id = id;
            Naam = naam;
            Email = email;
            Telefoon = telefoon;
        }

        public int Id { get; set; }
        public string Naam { get; set; }
        public string Email { get; set; }
        public string Telefoon { get; set; }
    }
}

