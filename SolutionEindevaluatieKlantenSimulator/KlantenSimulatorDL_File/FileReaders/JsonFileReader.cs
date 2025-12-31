using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Interfaces;
using System.Text.Json;
using static KlantenSimulatorBL.DTOs.JsonDTO;

namespace KlantenSimulatorDL_File.FileReaders
{
    public class JsonFileReader(INameReaderConfig config) : ICountryReader
    {
        private readonly INameReaderConfig _config = config;
        private FileJsonDTO? _jsonData;

        public CountryDTO ReadAddresses(string folder, string fileName, string countryName)
        {
            string jsonString = File.ReadAllText(Path.Combine(folder, fileName));

            var data = JsonSerializer.Deserialize<FileJsonDTO>(jsonString)!;

            CountryDTO country = new(countryName);

            foreach (var cityName in data.Address.City_Name)
            {
                CityDTO city = new(cityName);
                country.Cities.Add(city);
            }

            foreach (var streetName in data.Address.Street)
            {
                country.Addresses.Add(streetName);
            }

            _jsonData = data;

            return country;
        }

        public List<NameDTO.NameEntry> ReadNames(string folder, (string, string)[] fileNames, NameType nameType, Gender? gender)
        {
            List<NameDTO.NameEntry> names = [];

            var data = _jsonData;

            if (_jsonData == null)
            {
                foreach (var file in fileNames)
                {
                    string jsonString = File.ReadAllText(Path.Combine(folder, file.Item2));
                    data = JsonSerializer.Deserialize<FileJsonDTO>(jsonString);
                }
            }

            foreach (var property in typeof(NameSection).GetProperties()) //we need the section names so we can decide whether the names are of type first or last
            {
                //we know the property thanks to the typeof(NameSection) but now we want the values behind the property of NameSection, a list of strings (names)

                if (property.GetValue(data.Name) is not List<string> listOfNames || listOfNames.Count == 0)
                {
                    continue;
                }

                string propertyName = property.Name.ToLower();

                (nameType, gender) = GetNameTypeAndGender(propertyName);

                foreach (var name in listOfNames)
                {
                    names.Add(new NameDTO.NameEntry(name, nameType, gender, 1));
                }
            }

            return names;

        }

        private static (NameType, Gender?) GetNameTypeAndGender(string propertyName)
        {
            NameType nameType;
            Gender? gender;

            if (propertyName.StartsWith("first") || propertyName.EndsWith("first_name"))
            {
                nameType = NameType.First;
            }
            else
            {
                nameType = NameType.Last;
            }

            if (propertyName.EndsWith("male") || propertyName.StartsWith("male"))
            {
                gender = Gender.Male;
            }
            else if (propertyName.EndsWith("female") || propertyName.StartsWith("female"))
            {
                gender = Gender.Female;
            }
            else
            {
                gender = null;
            }

            return (nameType, gender);
        }

    }
}