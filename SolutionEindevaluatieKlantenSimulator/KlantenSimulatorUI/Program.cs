using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Manager;
using KlantenSimulatorUtils;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;

//build config

var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var config = builder.Build();

string? connectionString = config.GetConnectionString("SQLserver");

var countries = config.GetSection("AppSettings").GetChildren();

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

var repo = KlantenSimulatorSQLFactory.GetRepository(connectionString);
var manager = new DataManager(repo);

foreach (var countrySection in countries)
{
    Console.WriteLine("Loading: " + countrySection.Key);
    var countryEnum = Enum.Parse<Countries>(countrySection.Key);
    var countryReader = KlantenSimulatorCountryReaderFactory.GetCountryReader(countryEnum);

    string? folder = countrySection["Folder"];

    int datasetId = 0;

    //Addresses

    string? addressFile = countrySection["Addresses"] ?? countrySection["AddressesAndNames"];

    datasetId = manager.UploadAddresses(countryReader, folder, addressFile, countrySection.Key); //datasetid is required to give the same int to UploadNames

    var nameFiles = countrySection.GetChildren()
                                     .Where(c => c.Key.Contains("Name"))
                                     .Select(c => (c.Key, c.Value))
                                     .ToArray();

    if (nameFiles.Length != 0)
    {
        manager.UploadNames(countryReader, folder, nameFiles, datasetId, NameType.FirstLast, null);
    }
}





