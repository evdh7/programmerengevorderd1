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
            personeel.Add(new Personeel(101, "Linda", "Dialtone", new DateTime(1990, 3, 15), new Adres("Antwerpen", 2000, "Modemstraat", "56"), "linda_dialtone@aol.com"));
            personeel.Add(new Personeel(102, "Mark", "Floppy", new DateTime(1985, 6, 22), new Adres("Leuven", 3000, "Disketteplein", "9"), "mark.floppy@netscape.net"));
            personeel.Add(new Personeel(103, "Jenny", "Pixel", new DateTime(1992, 12, 5), new Adres("Namur", 5000, "CRTlaan", "3"), "jenny.pixel@hotmail.com"));
        }

        [Fact]

        public void VoegPersoneelToe_Valid_NoErrors()
        {
            Bedrijf bedrijf = new("Delhaize", "supermarkt", "voedingsindustrie", "Brussel", 1868, "voeding", new List<Personeel> { new UnitTestBedrijf().personeel[0]});
            var nieuwPersoneelslid = new Personeel(7, "Hans", "Badway", new DateTime(1987, 05, 26), new Adres("Brugge", 8200, "Straatnaam", "7"), "email@hotmail.com");


            bedrijf.VoegPersoneelToe(nieuwPersoneelslid);


            Assert.Contains(nieuwPersoneelslid, bedrijf.Personeel);
            Assert.Empty(bedrijf.Errors);

        }

        [Fact]
        public void VoegPersoneelToe_Invalid_ErrorsExist()
        {
            Bedrijf bedrijf = new("Delhaize", "supermarkt", "voedingsindustrie", "Brussel", 1868, "voeding", new List<Personeel> { new UnitTestBedrijf().personeel[0] });
            var nieuwPersoneelslid = new Personeel(7, "Hans", "Badway", new DateTime(2010, 05, 26), new Adres("Brugge", 8200, "Straatnaam", "7"), "email@hotmail.com");


            bedrijf.VoegPersoneelToe(nieuwPersoneelslid);


            Assert.Contains("personeelslid moet minstens 18 jaar zijn en geboortedatum mag niet leeg zijn", bedrijf.Errors);

        }
    }
}
