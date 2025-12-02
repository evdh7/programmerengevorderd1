namespace Vakantiepark2DL.TO
{
    public class WooneenheidTO
    {
        public WooneenheidTO(int id, string adres, int capaciteit, HuisStatus status)
        {
            Id = id;
            Adres = adres;
            Capaciteit = capaciteit;
            Status = status;
        }

        public int? Id { get; set; }
        public string Adres { get; set; }
        public int Capaciteit { get; set; }
        public HuisStatus Status { get; set; }
    }
}
