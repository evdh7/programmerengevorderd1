using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDL_File;
using Microsoft.Identity.Client;
using System.ComponentModel;
using System.Diagnostics;
using KlantenSimulatorBL.Exceptions;
using KlantenSimulatorBL.Enums;

namespace KlantenSimulatorUtils
{
    public static class KlantenSimulatorFileReaderFactory
    {
        public static IAddressReader GetAddressReader(string folder, string fileName, string country)
        {
            string extension = Path.GetExtension(fileName).ToLower(); //unit

            if (extension == ".csv")
            {
                return new CsvFileReader();
            }
            
            else if (extension == ".json")
            {
                return new JsonFileReader();
            }
            else
            {
                throw new KlantenSimulatorException($"No reader available for {fileName}"); //unit
            }
        }
       
        public static INameReader GetNameReader(string folder, string fileName, string country, NameType nameType)
        {
            string extension = Path.GetExtension(fileName).ToLower();

            if (extension == ".csv")
            {
                return new CsvFileReader();
            }

            else if (extension == ".json")
            {
                return new JsonFileReader();
            }

            else if (extension == ".txt" && country!= "Switserland")
            {
                return new TextFileReader();
            }

            return new TextNameByGenderFileReader();
        }


    }
}
