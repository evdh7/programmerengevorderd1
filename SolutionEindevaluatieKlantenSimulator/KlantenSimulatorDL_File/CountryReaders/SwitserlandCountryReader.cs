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
    public class SwitserlandCountryReader : ICountryReader
    {
        private readonly CsvFileReader _csvFileReader = new();
        private readonly TextNameByGenderFileReader _textNameByGenderFileReader = new();
        public CountryDTO ReadAddresses(string folder, string fileName, string country)
        {
            return _csvFileReader.ReadAddresses(folder, fileName, country);
        }
        public List<NameEntry> ReadNames(string folder, (string, string)[] fileNames, NameType nameType, Gender? gender)
        {
            var allNames = new List<NameEntry>();

            foreach ((string, string) file in fileNames)
            {
                if (file.Item1.Equals("LastNames"))
                {
                    allNames.AddRange(_textNameByGenderFileReader.ReadNames(folder, [file], NameType.Last, null));

                }
                else if (file.Item1.Equals("FirstNames"))
                {
                    allNames.AddRange(_textNameByGenderFileReader.ReadNames(folder, [file], NameType.First, null));
                }

            }
            return allNames;

        }
    }
}
