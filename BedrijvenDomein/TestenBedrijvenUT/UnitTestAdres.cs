using BedrijvenDomein;

namespace TestenBedrijvenUT
{
    public class UnitTestAdres
    {
        [Fact]
        public void Adres_Valid_NoErrors()
        {
            var adres = new Adres("Brugge", 8310, "Moerkerkse Steenweg", "1");
            Assert.Empty(adres.Errors);
        }

        [Fact]
        public void Adres_Invalid_ShouldCollectError()
        {
            var adres = new Adres("B", 831, "", "X");
            Assert.Contains("'woonplaats' heeft minder dan 2 karakters", adres.Errors);
            Assert.Contains("'postcode' ligt niet binnen bereik van 1000 - 9999", adres.Errors);
            Assert.Contains("'straatnaam' is vereist", adres.Errors);
            Assert.Contains("'huisnummer' vereist", adres.Errors);
        }

        [Theory]
        [InlineData("", 8310, "Maalse Steenweg", "3", "'woonplaats' heeft minder dan 2 karakters")]
        [InlineData("B", 8310, "Maalse Steenweg", "3", "'woonplaats' heeft minder dan 2 karakters")]
        [InlineData("Brugge", 831, "Maalse Steenweg", "3", "'postcode' ligt niet binnen bereik van 1000 - 9999")]
        [InlineData("Brugge", 83051, "Maalse Steenweg", "3", "'postcode' ligt niet binnen bereik van 1000 - 9999")]
        [InlineData("Brugge",  null, "Maalse Steenweg", "3", "'postcode' ligt niet binnen bereik van 1000 - 9999")]
        [InlineData("Brugge", 8310, "", "3", "'straatnaam' is vereist")]
        [InlineData("Brugge", 8310, "Maalse Steenweg" , "", "'huisnummer' vereist")]

        public void Adres_Invalid_ErrorsExist(string woonplaats, int postcode, string straatnaam, string huisnummer, string errorMessage)
        {
            var adres = new Adres(woonplaats, postcode, straatnaam, huisnummer);
            Assert.Contains(errorMessage, adres.Errors);
        }



    }
}