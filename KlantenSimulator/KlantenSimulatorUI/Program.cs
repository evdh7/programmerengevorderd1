using KlantenSimulatorBL.Beheer;
using KlantenSimulatorDL;

namespace KlantenSimulatorUI
{
    internal class Program
    {
        static void Main()
        {
            BestandLezer b1 = new BestandLezer();
            var vnm = b1.LeesNamen(@"C:\Users\emmy\source\PG1\dataSim\mannennamen_belgie.csv");
            var vnv = b1.LeesNamen(@"C:\Users\emmy\source\PG1\dataSim\vrouwennamen_belgie.csv");
            var fn = b1.LeesNamen(@"C:\Users\emmy\source\PG1\dataSim\Familienamen_2024_Belgie.csv");
            var pg = b1.LeesPostcodeGemeente(@"C:\Users\emmy\source\PG1\dataSim\zipcodes_alpha_nl_2025.csv");
            var sn = b1.LeesStraten(@"C:\Users\emmy\source\PG1\dataSim\adresInfo.txt");

            AdresSimulator adresSimulator = new(sn, pg, 300, 20);
            var a = adresSimulator.GeefAdressen(100);
            foreach(var item in a) {Console.WriteLine(item.ToString());}

            Console.WriteLine("end");
        }
    }
}
