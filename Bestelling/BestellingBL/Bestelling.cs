namespace BestellingBL
{
    public class Bestelling
    {
        private Dictionary<Product, int> producten = new();

        public Bestelling(DateTime bestelDatum)
        {
            BestelDatum = bestelDatum;
        }

        public int Id { get; set; }

        private DateTime bestelDatum;
        public DateTime BestelDatum
        {
            get { return bestelDatum; }
            set
            {
                if (value == default || bestelDatum < leverDatum) throw new BestellingException("besteldatum"); bestelDatum = value;
            }
        }
        private DateTime? leverDatum;
        public DateTime? LeverDatum
        {
            get { return leverDatum; }
            set
            {
                if (value!=DateTime.MinValue && value <= BestelDatum) throw new BestellingException("leverdatum"); leverDatum = value;
            }
        }
            
        public void VoegProductToe(Product product, int aantal)
        {
            if (aantal <= 0 || product == null) throw new BestellingException("voegproducttoe");
            if (producten.ContainsKey(product))//een object is hetzelfde als het op dezelfde plaats in het geheugen zit mrt containskey
            {
                producten[product] += aantal;
            }
            else
            {
                producten.Add(product, aantal);
            }
        }
        public void VerwijderProduct(Product product, int aantal)
        {
            if (product == null || aantal <= 0) throw new BestellingException("verwijderproduct");
            if (!producten.ContainsKey(product)) throw new BestellingException("verwijderproduct");
            if (producten[product] > aantal) // aantal hoger --> aantal aftrekken
            {
                producten[product]-= aantal;
            }
            else if (producten[product] == aantal) //aantal gelijk --> product verwijderen
            {
                producten.Remove(product);
            }
            else
            {
                throw new BestellingException("verwijderproducten");
            }


        }
        public double Prijs() { throw new NotImplementedException(); }
        public IReadOnlyList<(Product, int)> Producten()//tuple = lijst van parallele dingen - product int product item 1 en int item 2
        {
            return producten.Select(kvp => (kvp.Key, kvp.Value)).ToList();
        }

    }
}
