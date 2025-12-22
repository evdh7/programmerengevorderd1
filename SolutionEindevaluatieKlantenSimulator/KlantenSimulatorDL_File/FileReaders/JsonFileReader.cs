using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDL_File.Helpers.KlantenSimulatorDL_File.Helpers;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static KlantenSimulatorBL.DTOs.JsonDTO;

namespace KlantenSimulatorDL_File.FileReaders
{
    public class JsonFileReader : IAddressReader//INameReader
    {

        public CountryDTO ReadAddresses(string folder, string fileName, string countryName)
        {
            string jsonString = File.ReadAllText(Path.Combine(folder, fileName));

            var data = JsonSerializer.Deserialize<FileJsonDTO>(jsonString)!;

            CountryDTO country = new CountryDTO(countryName);

            foreach (var cityName in data.Address.City_Name)
            {
                CityDTO city = new CityDTO(cityName);
                country.Cities.Add(city);
            }

            foreach (var streetName in data.Address.Street)
            {
                country.Addresses.Add(streetName);
            }

            return country;
        }

        //public List<NameDTO.NameEntry> ReadNames(string folder, string fileName, NameType nameType, Gender gender)
        //{
        //    List<NameDTO.NameEntry> names = new();

        //    string jsonString = File.ReadAllText(Path.Combine(folder, fileName));
        //    Console.WriteLine(jsonString);
        //    var data = JsonSerializer.Deserialize<FileJsonDTO>(jsonString);
        //    nameType = NameType.First;
        //    foreach (var property in typeof(NameSection).GetProperties()) //we need the section names so we can decide whether the names are of type first or last
        //    {
        //        var listOfNames = property.GetValue(data.Name) as List<string>; //we know the property thanks to the typeof(NameSection) but now we want the values behind the property of NameSection, a list of strings (names)
        //        if (listOfNames == null)
        //        {
        //            continue;
        //        }

        //        string propertyName = property.Name.ToLower();

        //        nameType = Helper.GetNameType(propertyName);
        //        gender = Helper.GetGender(propertyName);

        //        foreach (var name in listOfNames)
        //        {
        //            names.Add(new NameDTO(name));
        //        }

        //        if (!result.ContainsKey(nameType))
        //        {
        //            result[nameType] = new List<NameDTO>();
        //        }

        //        result[nameType].AddRange(names);
        //    }
        //    result[nameType].AddRange(names);

        //    return result;

        //}
    }
}