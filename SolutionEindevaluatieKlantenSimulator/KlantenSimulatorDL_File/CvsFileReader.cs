using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDL_File.Helpers.KlantenSimulatorDL_File.Helpers;
using System.Net.Http.Headers;

namespace KlantenSimulatorDL_File
{
    public class CsvFileReader : IAddressReader, INameReader
    {
        public CountryDTO ReadAddresses(string folder, string fileName, string countryName)
        {

            var country = new CountryDTO(countryName);

            using (StreamReader sr = new StreamReader(Path.Combine(folder, fileName)))
            {
                string? line;
                bool firstLine = true;


                while ((line = sr.ReadLine()) != null)
                {

                    var validHighwayTypes = new List<string> { "residential", "tertiary", "secondary", "service" };

                    if (firstLine) //we skippen de eerste lijn
                    {
                        firstLine = false;
                        continue;
                    }

                    string[] ss = line.Split(';');
                    //use frequencyfinder like the textfilereader
                    if (ss[0].Contains("unknown"))
                        continue;

                    if (!validHighwayTypes.Any(type=> ss[2].Contains(type)))
                        continue;

                    string cityName = ss[0];

                    cityName = Helper.ExtractCityName(cityName, "kommune");
                    cityName = Helper.ExtractCityName(cityName, "kommun");

                    string streetName = ss[1];

                    var city = country.Cities.FirstOrDefault(c => c.Name == cityName);

                    if (city == null)
                    {
                        city = new CityDTO(cityName);
                        country.Cities.Add(city);
                    }

                    var existingStreet = city.Addresses.FirstOrDefault(a => a == streetName);

                    if (existingStreet == null)
                    {
                        city.Addresses.Add(streetName);
                    }


                }

                return country;

            }

        }

        public Dictionary<NameType, List<NameDTO>> ReadNames(string folder, string fileName, NameType nameType, Gender gender)
        {
            Dictionary<NameType, List<NameDTO>> result = new Dictionary<NameType, List<NameDTO>>();

            List<NameDTO> names = new List<NameDTO>();

            using (StreamReader sr = new StreamReader(Path.Combine(folder, fileName)))
            {

                string? line;             

                while ((line = sr.ReadLine()) != null)
                {
                    string[] ss = line.Split(';');
                    if (ss.Count() < 3) continue;

                    string name = ss[1];
                    int frequency = int.Parse(ss[2]);

                    names.Add(new NameDTO(name, gender, frequency));
                    
                }
                Console.WriteLine($"Status {nameType} {gender} {names.Count} names OK");
                result.Add(nameType, names);

            }

            Console.WriteLine(names.Count);
            return result;
        }
    }
}

