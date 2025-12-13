using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorBL.Manager;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorDL_File.Helpers.KlantenSimulatorDL_File.Helpers;
using KlantenSimulatorUtils;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.Design;
using System.Data;

//build config

var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var config = builder.Build();

string? connectionString = config.GetConnectionString("SQLserver");

var countries = config.GetSection("AppSettings").GetChildren();

foreach (var countrySection in countries)
{
    string country = countrySection.Key;
    string? folder = countrySection["Folder"];
    int datasetId = 0;

    foreach (var child in countrySection.GetChildren())
    {
        string sectionName = child.Key;   // "Addresses", "MaleNames", "FemaleNames"
        string? fileName = child.Value;   // "belgium_streets2.csv"

        if (string.IsNullOrWhiteSpace(fileName))
            continue;

        var repo = KlantenSimulatorSQLFactory.GetRepository(connectionString);
        var manager = new DataManager(repo);

        if (sectionName.ToLower().Contains("addresses"))
        {
            IAddressReader addressReader = KlantenSimulatorFileReaderFactory.GetAddressReader(folder, fileName, country);//we laten de klantensimulatorfilereaderfactory kiezen
            datasetId = manager.UploadAddresses(addressReader, folder, fileName, country);

            if (sectionName.ToLower().Contains("name"))
            {
                NameType nameType = Helper.GetNameType(sectionName);
                Gender genderType = Helper.GetGender(sectionName);
                INameReader nameReader = KlantenSimulatorFileReaderFactory.GetNameReader(folder, fileName, country, nameType);
                manager.UploadNames(nameReader, folder, fileName, datasetId, nameType, genderType);
            }

        }

        else if (sectionName.ToLower().Contains("name"))
        {
            NameType nameType = Helper.GetNameType(sectionName);
            Gender genderType = Helper.GetGender(sectionName);
            INameReader nameReader = KlantenSimulatorFileReaderFactory.GetNameReader(folder, fileName, country, nameType);
            manager.UploadNames(nameReader, folder, fileName, datasetId, nameType, genderType); 
        }
    }
}