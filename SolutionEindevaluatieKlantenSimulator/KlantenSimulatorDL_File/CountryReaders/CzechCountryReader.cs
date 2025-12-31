using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDL_File.FileReaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorDL_File.CountryReaders
{
    public class CzechCountryReader : ICountryReader
    {
        private readonly JsonFileReader _jsonFileReader;

        public CzechCountryReader()
        {
            _jsonFileReader = new JsonFileReader(new ReaderLayout(0, 0, null, null));

        }
        public CountryDTO ReadAddresses(string folder, string fileName, string country)
        {
            return _jsonFileReader.ReadAddresses(folder, fileName, country);
        }
        public List<NameDTO.NameEntry> ReadNames(string folder, (string, string)[] fileNames, NameType nameType, Gender? gender)
        {
            var allNames = new List<NameDTO.NameEntry>();

            foreach ((string, string) file in fileNames)
            {
                if (file.Item1.EndsWith("Names"))
                {
                    allNames.AddRange(_jsonFileReader.ReadNames(folder, [file], NameType.FirstLast, null));
                }

            }
            return allNames;


        }
    }
}
