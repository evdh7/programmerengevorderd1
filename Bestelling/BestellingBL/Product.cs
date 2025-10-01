
namespace BestellingBL
{
    public class Product
    {
        public Product(int id, string naam, double prijs)
        {
            Id = id;
            Naam = naam;
            Prijs = prijs;
        }

        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (value <= 0) throw new BestellingException("id<=0");
                id = value;
            }
        }

        private string naam;
        public string Naam
        {
            get { return naam; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Trim() != value) throw new BestellingException("naam");
                naam = value;
            }
        }

        private double prijs;
        public double Prijs
        {
            get { return prijs; }
            set
            {
                if (value < 0) throw new BestellingException("prijs<0");
                prijs = value;
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Product product &&
                   id == product.id &&
                   naam == product.naam &&
                   prijs == product.prijs;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id, naam, prijs);
        }
    }

}
