using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDL_File.Helpers.KlantenSimulatorDL_File.Helpers;

namespace KlantenSimulatorDL_File.FileReaders
{
    public class CsvFileReader(INameReaderConfig config) : INameReader, IAddressReader
    {
        private readonly INameReaderConfig _config = config;

        public CountryDTO ReadAddresses(string folder, string fileName, string countryName)
        {
            string searchString = _config.GetSearchString();

            var country = new CountryDTO(countryName);

            using StreamReader sr = new(Path.Combine(folder, fileName));
            string? line;
            bool firstLine = true;

            Dictionary<string, CityDTO> cityLookup = [];

            while ((line = sr.ReadLine()) != null)
            {

                var validHighwayTypes = new List<string> { "residential", "tertiary", "secondary", "service" };

                if (firstLine) //we skippen de eerste lijn
                {
                    firstLine = false;
                    continue;
                }

                string[] ss = line.Split(';');

                if (ss[0].Equals("(unknown)"))
                    continue;

                if (!validHighwayTypes.Any(type => ss[2].Equals(type)))
                    continue;

                string cityName = ss[0];

                if (!string.IsNullOrWhiteSpace(searchString))
                {
                    cityName = Helper.ExtractCityName(cityName, searchString);
                }

                string streetName = ss[1];

                if (!cityLookup.TryGetValue(cityName, out var city))
                {
                    city = new CityDTO(cityName);
                    cityLookup[cityName] = city;
                    country.Cities.Add(city);
                }
                
                city.Addresses.Add(streetName);
                
            }

            return country;

        }

        public List<NameDTO.NameEntry> ReadNames(string folder, (string, string)[] files, NameType type, Gender? gender)
        {
            List<NameDTO.NameEntry> names = [];

            foreach (var file in files)
            {

                using StreamReader sr = new(Path.Combine(folder, file.Item2));

                string? line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] ss = line.Split(';');
                    if (ss.Length < 3) continue;

                    string name = ss[1];
                    int frequency = int.Parse(ss[2]);

                    names.Add(new NameDTO.NameEntry(name, type, gender, frequency));

                }
            }
            return names;
        }
    }
}

