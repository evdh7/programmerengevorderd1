using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDL_File.FileReaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KlantenSimulatorBL.DTOs.NameDTO;

namespace KlantenSimulatorDL_File.CountryReaders
{
    public class PolandCountryReader : ICountryReader
    {
        private readonly CsvFileReader _csvFileReader;
        private readonly JsonFileReader _jsonFileReader;

        public PolandCountryReader()
        {
            _jsonFileReader = new JsonFileReader(new ReaderLayout(0, 0, null, null));

            _csvFileReader = new CsvFileReader(new ReaderLayout(0, 0, null, null));
        }
        public CountryDTO ReadAddresses(string folder, string fileName, string country)
        {
            return _csvFileReader.ReadAddresses(folder, fileName, country);
        }
        public List<NameEntry> ReadNames(string folder, (string, string)[] fileNames, NameType nameType, Gender? gender)
        {
            var allNames = new List<NameEntry>();

            foreach ((string, string) file in fileNames)
            {
                if (file.Item1.EndsWith("Names"))
                {
                    allNames.AddRange(_jsonFileReader.ReadNames(folder, [file] , NameType.FirstLast, null));

                }

            }
            return allNames;



        }
    }
}
