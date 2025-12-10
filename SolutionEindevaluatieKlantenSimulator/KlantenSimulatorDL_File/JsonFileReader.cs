using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDL_File.Helpers.KlantenSimulatorDL_File.Helpers;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static KlantenSimulatorBL.DTOs.JsonDTO;

namespace KlantenSimulatorDL_File
{
    public class JsonFileReader : IAddressReader, INameReader
    {

        public CountryDTO ReadAddresses(string folder, string fileName, string countryName)
        {
            string jsonString = File.ReadAllText(Path.Combine(folder, fileName));



            var data = JsonSerializer.Deserialize<FileJsonDTO>(jsonString)!;

            CountryDTO country = new CountryDTO(countryName);

            foreach (var cityName in data.Address.City_Names)
            {
                CityDTO city = new CityDTO(cityName);
                
            }

            foreach (var streetName in data.Address.Streets)
            {
                country.Addresses.Add(streetName);
            }
            return country;
        }

        public Dictionary<NameType, List<NameDTO>> ReadNames(string folder, string fileName, NameType nameType, Gender gender)
        {

            Dictionary<NameType, List<NameDTO>> result = new Dictionary<NameType, List<NameDTO>>();
            List<NameDTO> names = new List<NameDTO>();

            string jsonString = File.ReadAllText(Path.Combine(folder, fileName));
            var data = JsonSerializer.Deserialize<FileJsonDTO>(jsonString)!;

            foreach (var property in typeof(NameSection).GetProperties()) //we need the section names so we can decide whether the names are of type first or last
            {
                var listOfNames = property.GetValue(data.Name) as List<string>; //we know the property thanks to the typeof(NameSection) but now we want the values behind the property of NameSection, a list of strings (names)
                if (listOfNames != null)
                {
                    continue;
                }

                string propertyName = property.Name.ToLower();

                if (propertyName.Contains("first")) nameType = NameType.First;
                if (propertyName.Contains("last")) nameType = NameType.Last;

                foreach (var name in listOfNames)
                {

                    if (propertyName.Contains("male")) gender = Gender.Male;
                    if (propertyName.Contains("female")) gender = Gender.Female;

                    names.Add(new NameDTO(name, gender));
                }

                result.Add(nameType, names);
            }
                return result;
            
        }
    }
}