using BedrijvenDomein;
using System.ComponentModel.DataAnnotations;

namespace TestenBedrijvenUT
{
    public class UnitTestBedrijf
    {

        List<Personeel> personeel = new();
        public UnitTestBedrijf()
        {
            personeel.Add(new Personeel(11, "An", "Smith", new DateTime(1980, 11, 11), new Adres("Gent", 9000, "Lentestraat", "7"), "ann.smith@gmail.com"));
            personeel.Add(new Personeel(89, "Elsa", "Smet", new DateTime(1956, 10, 10), new Adres("Brugge", 8000, "Veemarkt", "17"), "frozen@gmail.com"));
            personeel.Add(new Personeel(76, "Olaf", "Smetterlinck", new DateTime(1989, 09, 27), new Adres("Brugge", 8200, "Jan Breydellaan", "248"), "olaf89@gmail.com"));
           
        }

        [Fact]
        public void Bedrijf_Valid_NoErrors()
        {
            Bedrijf bedrijf = new("Delhaize", "voedingsindustrie", "supermarkt", "Brussel", 1868, "voeding", new List<Personeel> { new UnitTestBedrijf().personeel[1] });
            Assert.Empty(bedrijf.Errors);

        }

        [Fact]

        public void BedrijfVoegPersoneelToe_Valid_NoErrors()
        {
            Bedrijf bedrijf = new("Delhaize", "voedingsindustrie", "supermarkt", "Brussel", 1868, "voeding", new List<Personeel> { new UnitTestBedrijf().personeel[0]});
            var nieuwPersoneelslid = new Personeel(7, "Hans", "Badway", new DateTime(1987, 05, 26), new Adres("Brugge", 8200, "Straatnaam", "7"), "email@hotmail.com");


            bedrijf.VoegPersoneelToe(nieuwPersoneelslid);


            Assert.Contains(nieuwPersoneelslid, bedrijf.Personeel);
            Assert.Empty(bedrijf.Errors);

        }

        [Fact]
        public void BedrijfVoegPersoneelToe_InvalidAgeErrors()
        {
            Bedrijf bedrijf = new("Delhaize", "voedingsindustrie", "supermarkt", "Brussel", 1868, "voeding", new List<Personeel> { new UnitTestBedrijf().personeel[0] });
            var nieuwPersoneelslid = new Personeel(7, "Hans", "Badway", new DateTime(2010, 05, 26), new Adres("Brugge", 8200, "Straatnaam", "7"), "email@hotmail.com");


            bedrijf.VoegPersoneelToe(nieuwPersoneelslid);


            Assert.Contains("personeelslid moet minstens 18 jaar zijn", bedrijf.Errors);
            Assert.Contains("--->Fout bij inlezen van personeel<---", bedrijf.Errors);

        }

        [Fact]
        public void BedrijfVoegPersoneelToe_Invalid_PersonAlreadyExistError()
        {
            Bedrijf bedrijf = new("Delhaize", "voedingsindustrie", "supermarkt", "Brussel", 1868, "voeding", new List<Personeel> { new UnitTestBedrijf().personeel[0] });
            var personeel = new Personeel(11, "An", "Smith", new DateTime(1980, 11, 11), new Adres("Gent", 9000, "Lentestraat", "7"), "ann.smith@gmail.com");

            bedrijf.VoegPersoneelToe(personeel);


            Assert.Contains("'personeelslid' bestaat al", bedrijf.Errors);

        }


        [Fact]
        public void Bedrijf_Invalid_PersoneelEmptyError ()
        {
            Bedrijf bedrijf = new("Delhaize", "voedingsindustrie", "supermarkt", "Brussel", 1868, "voeding", null);

            Assert.Contains("Een bedrijf moet minstens 1 personeelslid hebben", bedrijf.Errors);

        }

        [Theory]
        [InlineData("", "voedingsindustrie", "supermarkt", "Brussel", 1868, "voeding", "'bedrijfsnaam' is vereist.")]
        [InlineData("Delhaize", "", "supermarkt", "Brussel", 1868, "voeding", "'industrie' is vereist.")]
        [InlineData("Delhaize", "voedingsindustrie", "", "Brussel", 1868, "voeding", "'sector' is vereist.")]
        [InlineData("Delhaize", "voedingsindustrie", "supermarkt", "", 1868, "voeding", "'hoofdkwartier' is vereist.")]
        [InlineData("Delhaize", "voedingsindustrie", "supermarkt", "Brussel", 0, "voeding", "'jaar' is vereist")]
        [InlineData("Delhaize", "voedingsindustrie", "supermarkt", "Brussel", 2027, "voeding", "'jaar' mag niet in de toekomst liggen")]
        [InlineData("Delhaize", "voedingsindustrie", "supermarkt", "Brussel", 1868, "", "'extra' is vereist.")]

        public void Bedrijf_Invalid_DataError(string naam, string industrie, string sector, string hoofdkwartier, int jaaroprichting, string extra, string errors)
        {
            Bedrijf bedrijf = new(naam, industrie, sector, hoofdkwartier, jaaroprichting, extra, new List<Personeel> { new UnitTestBedrijf().personeel[0] });
            Assert.Contains(errors, bedrijf.Errors);
        }




    }
}
