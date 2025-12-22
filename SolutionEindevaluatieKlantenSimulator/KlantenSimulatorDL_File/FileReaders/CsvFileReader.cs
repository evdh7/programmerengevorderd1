using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDL_File.Helpers.KlantenSimulatorDL_File.Helpers;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using static KlantenSimulatorBL.DTOs.NameDTO;

namespace KlantenSimulatorDL_File.FileReaders
{
    public class CsvFileReader : INameReader, IAddressReader
    {
        public CountryDTO ReadAddresses(string folder, string fileName, string countryName)
        {

            var country = new CountryDTO(countryName);

            using (StreamReader sr = new StreamReader(Path.Combine(folder, fileName),Encoding.UTF8))
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
                    if (ss[0].Equals("(unknown)"))
                        continue;

                    if (!validHighwayTypes.Any(type => ss[2].Equals(type)))
                        continue;

                    string cityName = ss[0];

                    cityName = Helper.ExtractCityName(cityName, "kommune");
                    cityName = Helper.ExtractCityName(cityName, "kommun");

                    string streetName = ss[1];

                    Dictionary<string, CityDTO> cityLookup = new();


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

        }

        //public Dictionary<NameType, List<NameDTO>> ReadNames(string folder, string fileName, NameType nameType, Gender gender)
        //{
        //    Dictionary<NameType, List<NameDTO>> result = new Dictionary<NameType, List<NameDTO>>();

        //    List<NameDTO> names = new List<NameDTO>();

        //    using (StreamReader sr = new StreamReader(Path.Combine(folder, fileName)))
        //    {

        //        string? line;             

        //        while ((line = sr.ReadLine()) != null)
        //        {
        //            string[] ss = line.Split(';');
        //            if (ss.Count() < 3) continue;

        //            string name = ss[1];
        //            int frequency = int.Parse(ss[2]);

        //            names.Add(new NameDTO(name, gender, frequency));
                    
        //        }
        //        Console.WriteLine($"Status {nameType} {gender} {names.Count} names OK");
        //        result.Add(nameType, names);

        //    }

        //    Console.WriteLine(names.Count);
        //    return result;
        //}

        public List<NameEntry> ReadNames(string folder, (string, string)[] fileNames, NameType type, Gender? gender)
        {
            List <NameEntry> names = new();

            foreach (var file in fileNames)
            {

                using (StreamReader sr = new StreamReader(Path.Combine(folder, file.Item2),Encoding.UTF8))
                {

                    string? line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] ss = line.Split(';');
                        if (ss.Count() < 3) continue;

                        string name = ss[1];
                        int frequency = int.Parse(ss[2]);

                        names.Add(new NameEntry(name, type, gender, frequency));

                    }

                }
            }
            return names;
        }
    }
}

