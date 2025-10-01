using BestellingBL;

namespace TestBestellingBL3
{
    public class UnitTestBestelling
    {
        List<Product> producten = new();

        public UnitTestBestelling()
        {
            producten.Add(new Product(1, "kast", 300));
            producten.Add(new Product(2, "deur", 400));
            producten.Add(new Product(3, "stoel", 500));
            producten.Add(new Product(1, "kast", 300));

        }

        [Fact]
        public void Test_besteldatum_valid()
        {
            DateTime now = DateTime.Now;
            Bestelling bestelling = new Bestelling(now);
            Assert.Equal(now, bestelling.BestelDatum);
        }
        [Fact]
        public void Test_besteldatum_invalid()
        {
            DateTime now = DateTime.Now;
            Bestelling bestelling = new Bestelling(now);
            Assert.Throws<BestellingException>(() =>
                bestelling.BestelDatum = default);
        }
        [Fact]
        public void Test_besteldatum_invalid_vergelijkmetleverdatum()
        {
            DateTime now = DateTime.Now;
            Bestelling bestelling = new Bestelling(now);
            bestelling.LeverDatum = now.AddHours(1);
            Assert.Throws<BestellingException>(() =>
                bestelling.BestelDatum = now.AddHours(1));
        }

        [Theory]
        [InlineData(default)]
        [InlineData("2025-1-2")]

        public void Test_leverdatum_valid(DateTime dateTime)
        {
            DateTime now = new DateTime(2025, 1, 1);
            Bestelling bestelling = new Bestelling(now);
            bestelling.LeverDatum = dateTime;
            Assert.Equal(dateTime, bestelling.LeverDatum);
        }

        [Theory]
        [InlineData("2025-1-2")] // zelfde waarde
        [InlineData("2025-1-1")] // vroeger dan
        public void Test_leverdatum_invalid(DateTime dateTime)
        {
            DateTime now = new DateTime(2025, 1, 2);
            Bestelling bestelling = new Bestelling(now);
            Assert.Throws<BestellingException>(()
                => bestelling.LeverDatum = dateTime);
        }
        [Fact]
        public void Test_voegproductToe_valid()
        {
            DateTime now = DateTime.Now;
            Bestelling bestelling = new Bestelling(now);
            //deel 1
            bestelling.VoegProductToe(producten[0], 1);
            Assert.Equal((producten[0], 1), bestelling.Producten().First());
            Assert.Single(bestelling.Producten());

            //deel2
            bestelling.VoegProductToe(producten[0], 4);
            Assert.Equal((producten[0], 5), bestelling.Producten().First()); //was het 3 of 0?
            Assert.Single(bestelling.Producten());

            //deel 3
            bestelling.VoegProductToe(producten[2], 4);
            Assert.Equal(2, bestelling.Producten().Count());
            Assert.Contains((producten[0], 5), bestelling.Producten());
            Assert.Contains((producten[2], 4), bestelling.Producten());

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Test_voegproducttoe_invalid_aantal(int aantal)
        {
            DateTime now = DateTime.Now;
            Bestelling bestelling = new Bestelling(now);
            bestelling.VoegProductToe(producten[0], 5);
            bestelling.VoegProductToe(producten[2], 4);

            Assert.Throws<BestellingException>(() => bestelling.VoegProductToe(producten[0], aantal));
           
            Assert.Equal(2, bestelling.Producten().Count());
            Assert.Contains((producten[0], 5), bestelling.Producten());
            Assert.Contains((producten[2], 4), bestelling.Producten());

        }

        [Fact]
        public void Test_voegproducttoe_invalid_productnull()
        {
            DateTime now = DateTime.Now;
            Bestelling bestelling = new Bestelling(now);
            bestelling.VoegProductToe(producten[0], 5);
            bestelling.VoegProductToe(producten[2], 4);

            Assert.Throws<BestellingException>(() => bestelling.VoegProductToe(null, 10));
            Assert.Equal(2, bestelling.Producten().Count());
            Assert.Contains((producten[0], 5), bestelling.Producten());
            Assert.Contains((producten[2], 4), bestelling.Producten());

        }

        [Fact]

        public void Test_verwijderproduct_valid()
        {

            //set up
            DateTime now = DateTime.Now;
            Bestelling bestelling = new Bestelling(now);
            bestelling.VoegProductToe(producten[0], 5);
            bestelling.VoegProductToe(producten[2], 4);

            //deel 1
            bestelling.VerwijderProduct(producten[2], 3);
            Assert.Equal(2, bestelling.Producten().Count());
            Assert.Contains((producten[0], 5), bestelling.Producten());
            Assert.Contains((producten[2], 1), bestelling.Producten());

            //deel 2

            bestelling.VerwijderProduct(producten[2], 1);
            Assert.Single(bestelling.Producten());
            Assert.Contains((producten[0], 5), bestelling.Producten());

            // deel 3

            bestelling.VerwijderProduct(producten[0], 5);
            Assert.Empty(bestelling.Producten());
        }

        [Fact]
        public void Test_verwijderproduct_invalid_productbestaatniet()
        {

            DateTime now = DateTime.Now;
            Bestelling bestelling = new(now);
            bestelling.VoegProductToe(producten[0], 5);
            bestelling.VoegProductToe(producten[2], 4);

            // deel 1 : product (aantal) verwijderen

            Assert.Throws<BestellingException>(() => bestelling.VerwijderProduct(producten[1], 3));
            Assert.Equal(2, bestelling.Producten().Count());
            Assert.Contains((producten[0], 5), bestelling.Producten());
            Assert.Contains((producten[2], 4), bestelling.Producten());

        }

        [Fact]
        public void Test_verwijderproduct_invalid_aantaltegroot()
        {
            DateTime now = DateTime.Now;
            Bestelling bestelling = new(now);
            bestelling.VoegProductToe(producten[0], 5);
            bestelling.VoegProductToe(producten[2], 4);

            // deel 1 : product (aantal) verwijderen

            Assert.Throws<BestellingException>(() => bestelling.VerwijderProduct(producten[0], 6));

            Assert.Equal(2, bestelling.Producten().Count());
            Assert.Contains((producten[0], 5), bestelling.Producten());
            Assert.Contains((producten[2], 4), bestelling.Producten());

        }
    }
}

