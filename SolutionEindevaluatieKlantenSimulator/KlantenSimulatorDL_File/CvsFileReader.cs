using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDL_File.Helpers.KlantenSimulatorDL_File.Helpers;

namespace KlantenSimulatorDL_File
{
    public class CvsFileReader : IFileReader
    {
        public CountryDTO ReadAddresses(string folder, List<string> fileNames, string countryName)
        {

            var country = new CountryDTO(countryName);

            using (StreamReader sr = new StreamReader(Path.Combine(folder, fileNames[0])))
            {
                string line;
                bool firstLine = true;


                while ((line = sr.ReadLine()) != null)
                {
                    if (firstLine) //we skippen de eerste lijn
                    {
                        firstLine = false;
                        continue;
                    }

                    string[] ss = line.Split(';');

                    if (!ss[2].Contains("residential"))
                    {
                        continue;
                    }

                    string cityName = ss[0];
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


        

        public List<FirstNameDTO> ReadFirstNames(string folder, List<string> fileNames)
        {
            List<FirstNameDTO> firstNames = new List<FirstNameDTO>();
            int[] indeces = { 2, 3 };

            foreach (int index in indeces)
            {
                using (StreamReader sr = new StreamReader(Path.Combine(folder, fileNames[index])))
                {
                    Gender gender = Helper.GetGender(fileNames[index]);
                    string? line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] ss = line.Split(';');
                        if (ss.Count() < 3) continue;

                        string name = ss[1];
                        int frequency = int.Parse(ss[2]);

                        firstNames.Add(new FirstNameDTO(name, gender, frequency));
                    }
                    
                }
                
            }
            return firstNames;
        }

        public List<LastNameDTO> ReadLastNames(string folder, List<string> fileNames)
        {
            List<LastNameDTO> lastNames = new List<LastNameDTO>();

            using (StreamReader sr = new StreamReader(Path.Combine(folder, fileNames[1])))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] ss = line.Split(';');
                    string name = ss[1];
                    int frequency = int.Parse(ss[2]);

                    lastNames.Add(new LastNameDTO(name, frequency));
                }
                return lastNames;
            }
        }
    }
}

