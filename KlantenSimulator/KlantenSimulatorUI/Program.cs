using KlantenSimulatorBL.Beheer;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDL;
using Microsoft.Extensions.Configuration;

namespace KlantenSimulatorUI
{
    internal class Program
    {
        static void Main()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration=builder.Build();

            string padVoornamenMannen = configuration.GetSection("AppSettings")["padVoornamenMannen"];
            string padVoornamenVrouwen = configuration.GetSection("AppSettings")["padVoornamenVrouwen"];
            int aantalAdressen = int.Parse(configuration.GetSection("AppSettings")["aantalAdressen"]);
            var padVoorFamilienamen = configuration.GetSection("AppSettings")["padVoorFamilienamen"]
            //Console.WriteLine(padVoornamenMannen);
            //Console.WriteLine(padVoornamenVrouwen);
            //Console.WriteLine(aantalAdressen);


            Databeheerder dataBeheerder = new Databeheerder(new BestandsLezer(), new BestandSchrijver(), 10, 100, 10000, 100000,
                padVoornamenMannen, padVoornamenVrouwen, padVoorFamilienamen, 20,300,
                @"C:\Users\emmy\source\PG1\dataSim\adresInfo.txt", @"C:\Users\emmy\source\PG1\dataSim\zipcodes_alpha_nl_2025.csv", aantalAdressen);

            dataBeheerder.SimuleerPersonen(50, @"C:\Users\emmy\source\PG1\dataSim\outSim.txt");
            //BestandsLezer b1 = new BestandsLezer();
            //var vnm = b1.LeesNamen(@"C:\Users\emmy\source\PG1\dataSim\mannennamen_belgie.csv");
            //var vnv = b1.LeesNamen(@"C:\Users\emmy\source\PG1\dataSim\vrouwennamen_belgie.csv");
            //var fn = b1.LeesNamen(@"C:\Users\emmy\source\PG1\dataSim\Familienamen_2024_Belgie.csv");
            //var pg = b1.LeesPostcodeGemeente(@"C:\Users\emmy\source\PG1\dataSim\zipcodes_alpha_nl_2025.csv");
            //var sn = b1.LeesStraten(@"C:\Users\emmy\source\PG1\dataSim\adresInfo.txt");

            //AdresSimulator adresSimulator = new(sn, pg, 300, 20);
            //var a = adresSimulator.GeefAdressen(10000);
            //// foreach(var item in a) {Console.WriteLine(item.ToString());}
            //vnv.AddRange(vnm);
            //PersoonSimulator persoonSimulator = new PersoonSimulator(vnv, fn, a, 10, 100, 10000, 100000);
            //var p = persoonSimulator.MaakPersoon(25);
            //foreach (var o in p) Console.WriteLine(o.ToString());
            //BestandSchrijver bs = new BestandSchrijver();
            //bs.SchrijfBestand(p, @"C:\Users\emmy\source\PG1\dataSim\outSim.txt");
            Console.WriteLine("end");
        }
    }
}
