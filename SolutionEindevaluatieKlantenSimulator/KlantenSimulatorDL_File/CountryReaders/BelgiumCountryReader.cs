using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDL_File.FileReaders;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using static KlantenSimulatorBL.DTOs.NameDTO;

namespace KlantenSimulatorDL_File.CountryReaders
{

    public class BelgiumCountryReader : ICountryReader
    {
        private readonly CsvFileReader _csvFileReader = new CsvFileReader();

        public CountryDTO ReadAddresses(string folder, string fileName, string country)
        {
            return _csvFileReader.ReadAddresses(folder, fileName, country);
        }
        public List<NameEntry> ReadNames(string folder, (string, string)[] fileNames, NameType type, Gender? gender)
        {
            var allNames = new List<NameEntry>();

             foreach ((string, string) file in fileNames)
            {
                if (file.Item1.Equals("LastNames"))
                {
                    allNames.AddRange(_csvFileReader.ReadNames(folder, new[] { file }, NameType.Last, null));

                }
                else if (file.Item1.Equals("MaleFirstNames"))
                {
                    allNames.AddRange(_csvFileReader.ReadNames(folder, [file], NameType.First, Gender.Male));
                }

                else if (file.Item1.Equals("FemaleFirstNames"))
                {
                    allNames.AddRange(_csvFileReader.ReadNames(folder, [file], NameType.First, Gender.Female));
                }

            }

            return allNames;
        }
    }
}

