using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Manager;
using KlantenSimulatorUtils;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Runtime.InteropServices;

//build config

var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var config = builder.Build();

string? connectionString = config.GetConnectionString("SQLserver");

var countries = config.GetSection("AppSettings").GetChildren();


foreach (var countrySection in countries)
{
    var countryEnum = Enum.Parse<Countries>(countrySection.Key);
    var countryReader = KlantenSimulatorCountryReaderFactory.GetCountryReader(countryEnum);

    string? folder = countrySection["Folder"];

    var repo = KlantenSimulatorSQLFactory.GetRepository(connectionString);
    var manager = new DataManager(repo);
    int datasetId = 0;

    //Addresses

    var addressFile = countrySection["Addresses"];

    datasetId = manager.UploadAddresses(countryReader, folder, addressFile, countrySection.Key); //datasetid is required to give the same int to UploadNames

    var nameFiles = countrySection.GetChildren()
                                     .Where(c => c.Key.Contains("Name"))
                                     .Select(c => (Key: c.Key, Value: c.Value))
                                     .ToArray();

    if (nameFiles.Any())
    {
        manager.UploadNames(countryReader, folder, nameFiles, datasetId, NameType.FirstLast, null); 
    }
}


