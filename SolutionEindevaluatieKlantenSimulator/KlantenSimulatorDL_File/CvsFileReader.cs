using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Interfaces;
using System.Globalization;
using System.Reflection;

namespace KlantenSimulatorDL_File
{
    public class CvsFileReader : IFileReader
    {
        public List<AddressDTO> ReadAddresses(string folder, List<string> fileNames, string country)
        {
            List<AddressDTO> addresses = new List<AddressDTO>();

            using (StreamReader sr = new StreamReader(Path.Combine(folder, fileNames[0])))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] ss = line.Split(';');
                    string city = ss[0];
                    string street = ss[1];

                    addresses.Add(new AddressDTO(country, city, street));
                }
                return addresses;
            }


        }

        public List<FirstNameDTO> ReadFirstNames(string folder, List<string> fileNames, string country)
        {
            List<FirstNameDTO> firstNames = new List<FirstNameDTO>();

            using (StreamReader sr = new StreamReader(Path.Combine(folder, fileNames[2])))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] ss = line.Split(';');
                    string name = ss[1];
                    int frequency = int.Parse(ss[2]);

                    firstNames.Add(new FirstNameDTO(name, gender, frequency, country));
                }
            }

            using (StreamReader sr = new StreamReader(Path.Combine(folder, fileNames[3])))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] ss = line.Split(';');
                    string name = ss[1];
                    int frequency = int.Parse(ss[2]);

                    firstNames.Add(new FirstNameDTO(name, gender, frequency, country));
                }
                return firstNames;
            }
        }

        public List<LastNameDTO> ReadLastNames(string folder, List<string> fileNames, string country)
        {
            List<LastNameDTO> lastNames = new List<LastNameDTO>();

            using (StreamReader sr = new StreamReader(Path.Combine(folder, fileNames[1])))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string gender = fileNames[0].ToLower().Contains("male") ? "M" : "F";
                    string[] ss = line.Split(';');
                    string name = ss[1];
                    int frequency = int.Parse(ss[2]);

                    lastNames.Add(new LastNameDTO(name, frequency, country));
                }
                return lastNames;
            }
        }
    }
}

