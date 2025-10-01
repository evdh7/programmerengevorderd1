using BestellingBL;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace TestBestellingBL3
{
    public class UnitTestProduct
    {
        [Theory]
        [InlineData(1)]
        [InlineData(200)]
        public void Test_id_valid(int id)
        {
            Product p = new Product(10, "zetel",125.5);
            p.Id = id;
            Assert.Equal(id, p.Id);
            
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Test_id_invalid(int id)
        {
            Product p = new Product(10, "zetel", 125.5);
            Assert.Throws<BestellingException>(() => p.Id = id);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(10.5)]
        public void Test_prijs_valid(double prijs)
        {
            Product p = new Product(10, "zetel", 125.5);
            p.Prijs = prijs;
            Assert.Equal(prijs, p.Prijs);
        }

        [Theory]
        [InlineData(-1)]
        public void Test_prijs_invalid(double prijs)
        {
            Product p = new Product(10, "zetel", 125.5);
            Assert.Throws<BestellingException>(() => p.Prijs = prijs);
        }

        [Theory]
        [InlineData("zetel")]
        [InlineData("stoel en tafel")]
        public void Test_naam_valid(string naam)
        {
            Product p = new Product(10, "zetel", 125.5);
            p.Naam = naam;
            Assert.Equal(naam, p.Naam);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(" stoel en tafel")]
        [InlineData("stoel en tafel ")]
        [InlineData(" stoel en tafel ")]
        [InlineData(null)]


        public void Test_naam_invalid(string naam)
        {
            Product p = new Product(10, "zetel", 125.5);
            Assert.Throws<BestellingException>(()=>p.Naam = naam);
        }
        [Fact]
        public void Test_ctor_valid()
        {
            Product p = new Product(10, "zetel", 125.5);
            Assert.Equal(10,p.Id);
            Assert.Equal("zetel", p.Naam);
            Assert.Equal(125.5, p.Prijs);

        }

        [Theory]
        [InlineData("",1,10)]
        [InlineData(" stoel en tafel",1,10)]
        [InlineData("stoel en tafel ",1,10)]
        [InlineData(" ", 1,10)]
        [InlineData(null,1,10)]
        [InlineData("zetel",-1, 10)]
        [InlineData("zetel", 1, -0.01)]


        public void Test_ctor_invalid(string naam, int id, double prijs)
        {
            Assert.Throws<BestellingException>(() => new Product(id, naam, prijs));
        }

       

    }
}