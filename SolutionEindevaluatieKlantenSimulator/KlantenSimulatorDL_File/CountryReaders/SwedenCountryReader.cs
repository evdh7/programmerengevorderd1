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
    public class SwedenCountryReader : ICountryReader
    {
        private readonly CsvFileReader _csvFileReader;
        private readonly TextFileReader _textFileReader;

        public SwedenCountryReader()
        {
            _csvFileReader = new CsvFileReader(new ReaderLayout(0, 0, null, "kommun"));
            _textFileReader = new TextFileReader(new ReaderLayout(0, 1, null, ""));
        }
        public CountryDTO ReadAddresses(string folder, string fileName, string country)
        {
            return _csvFileReader.ReadAddresses(folder, fileName, country);
        }

        public List<NameDTO.NameEntry> ReadNames(string folder, (string, string)[] fileNames, NameType nameType, Gender? gender)
        {
            var allNames = new List<NameEntry>();

            foreach ((string, string) file in fileNames)
            {
                if (file.Item1.Equals("LastNames"))
                {
                    allNames.AddRange(_textFileReader.ReadNames(folder, [file], NameType.Last, null));

                }
                else if (file.Item1.Equals("MaleFirstNames"))
                {
                    allNames.AddRange(_textFileReader.ReadNames(folder, [file], NameType.First, Gender.Male));
                }

                else if (file.Item1.Equals("FemaleFirstNames"))
                {
                    allNames.AddRange(_textFileReader.ReadNames(folder, [file], NameType.First, Gender.Female));
                }

            }
            return allNames;
        }
    }
}
