using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDL_File.FileReaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorDL_File.CountryReaders
{
    public class PolandCountryReader : ICountryReader
    {
        private readonly CsvFileReader _csvFileReader = new CsvFileReader();
        private readonly JsonFileReader _jsonFileReader = new JsonFileReader();

        public CountryDTO ReadAddresses(string folder, string fileName, string country)
        {
            return _csvFileReader.ReadAddresses(folder, fileName, country);
        }



    }
}
