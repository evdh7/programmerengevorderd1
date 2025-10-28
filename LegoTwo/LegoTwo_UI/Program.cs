using Microsoft.Extensions.Configuration;
using LegoTwo_BL.Beheer;
using LegoTwo_Util;

namespace LegoTwo_UI;
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        var config = builder.Build();

        string connectionString = config.GetConnectionString("SQLserver");
        string pad = config.GetSection("AppSettings")["lego_sets"];

        LegoBeheerder legoBeheerder = new LegoBeheerder
            (LegoTwoFileReaderFactory.GiveFileReader(), LegoTwoRepositoryFactory.GiveRepository
            (connectionString));
        legoBeheerder.UploadNaarDataBank(pad);
    }
}