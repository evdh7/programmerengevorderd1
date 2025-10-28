using ProvinciesBL.Exceptions;

namespace ProvinciesBL.Model
{
    public class Gemeente
    {
        public Gemeente(string naam, List<Straat> data)
        {
            Naam = naam;
            if (data == null || data.Count == 0)//als er geen straat is
            {
                throw new ProvincieException("Straat niet ok");
            }
            foreach (var g in data) VoegStraatToe(g);

        }

        public int? Id { get; set; }
        public string Naam { get; set; }

        private List<Straat> straten = new();
        public IReadOnlyList<Straat> Straten => straten;
        public void VoegStraatToe(Straat straat)
        {
            if (straten == null) { throw new ProvincieException("straten is null"); }
            straten.Add(straat);
        }
    }
}