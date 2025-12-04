using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorBL.Manager;
using KlantenSimulatorUtils;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KlantenSimulatorUI
{
    internal class Program
    {
        static void Main()
        {

            //build config

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var config = builder.Build();

            string connectionString = config.GetConnectionString("SQLserver");

            var appSettings = config.GetSection("AppSettings");
            foreach (var countrySection in appSettings.GetChildren())
            {
                string country = countrySection.Key;
                string folder = countrySection["Folder"];

                var fileNames = new List<string>();
                AddIfExists(fileNames, countrySection["Addresses"]);
                AddIfExists(fileNames, countrySection["MaleNames"]);
                AddIfExists(fileNames, countrySection["FemaleNames"]);
                AddIfExists(fileNames, countrySection["LastNames"]);
                AddIfExists(fileNames, countrySection["Names"]);
                AddIfExists(fileNames, countrySection["LastNames20"]);
                AddIfExists(fileNames, countrySection["Data"]);
                AddIfExists(fileNames, countrySection["FirstNames"]);


                //string folder = config.GetSection("AppSettings")["Folder"];
                //List<string> fileNames = new List<string>();
                //fileNames.Add(config.GetSection("AppSettings")["Addresses"]);
                //fileNames.Add(config.GetSection("AppSettings")["LastNames"]);
                //fileNames.Add(config.GetSection("AppSettings")["MaleNames"]);
                //fileNames.Add(config.GetSection("AppSettings")["FemaleNames"]);

                //string country = "belgie";


                foreach (var fileName in fileNames) //kiezen welke reader we doorgeven voor welk bestand
                {
                    IFileReader reader = KlantenSimulatorFileReaderFactory.GetFileReader(fileName); //we laten de klantensimulatorfilereaderfactory kiezen
                    DataManager manager = new DataManager(reader, KlantenSimulatorSQLFactory.GetRepository(connectionString)); //die geven we door aan de datamanager
                    manager.UploadToDatabase(folder, fileNames, country);
                }

            }
        }
        private static void AddIfExists(List<string> files, string? value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                files.Add(value);
        }
    }
}

