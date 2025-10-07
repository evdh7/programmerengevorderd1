using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BedrijvenDomein;

namespace TestenBedrijvenUT
{
    public class UnitTestPersoneel
    {
        private const string B = "Brugge";

        Adres adres = new Adres("Brugge", 8310, "Moerkerkse Steenweg", "1");

        [Fact]
        public void Personeel_Valid_NoErrors()
        {
            var personeel = new Personeel(11, "An", "Smith", new DateTime(2000, 11, 11), new Adres("Gent", 9000, "Lentestraat", "7"), "ann.smith@gmail.com");
            Assert.Empty(personeel.Errors);
        }

        [Fact]
        public void Personeel_Invalid_ShouldCollectError()
        {
            var personeel = new Personeel(-1, "", "Smith", new DateTime(2027, 11, 11), new Adres("Gent", 9000, "Lentestraat", "7"), "ann.smith@");
            Assert.Contains("ID nummer moet groter zijn dan 0", personeel.Errors);
            Assert.Contains("'voornaam' is vereist", personeel.Errors);
            Assert.Contains("personeelslid moet minstens 18 jaar zijn en geboortedatum mag niet leeg zijn", personeel.Errors);
            Assert.Contains("email heeft geen geldige structuur", personeel.Errors);
        }

        [Theory]
        [InlineData("ann.smithgmail.com", "email heeft geen geldige structuur")]
        [InlineData("ann@smith", "email heeft geen geldige structuur")]              
        [InlineData("ann.smith@.com", "email heeft geen geldige structuur")]          
        [InlineData("", "email mag niet leeg zijn")]                        
        public void Personeel_InvalidEmail_ShouldCollectError(string email, string errorMessage)
        {
            var adres = new Adres("Gent", 9000, "Lentestraat", "7");
            var personeel = new Personeel(1, "Ann", "Smith", new DateTime(1990, 5, 1), adres, email);

            Assert.Contains(errorMessage, personeel.Errors);
        }

        [Fact]

        public void Personeel_Invalid_NoAdressError()
        {
            var personeel = new Personeel(11, "An", "Smith", new DateTime(2000, 11, 11), null, "ann.smith@gmail.com");
            Assert.Contains("adres is null", personeel.Errors);
        }


    }
}
