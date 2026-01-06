using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDL_File.Helpers.KlantenSimulatorDL_File.Helpers;
using System.Globalization;
using System.Text;
using static KlantenSimulatorBL.DTOs.NameDTO;

namespace KlantenSimulatorDL_File.FileReaders
{
    public class TextFileReader(INameReaderConfig config) : INameReader
    {
        private readonly INameReaderConfig _config = config;

        public List<NameEntry> ReadNames(string folder, (string, string)[] fileNames, NameType nameType, Gender? gender)
        {
            uint fColumn = _config.GetFrequencyColumn();
            uint nColumn = _config.GetNameColumn();

            List<NameEntry> allNames = [];

            foreach (var file in fileNames)

            {
                using StreamReader sr = OpenReader(folder, file.Item2);
                
                string[]? firstValidLine = Helper.SkipLines(sr);

                if (firstValidLine != null)
                { 
                    Helper.ParseLine(firstValidLine, gender, allNames, nameType, fColumn, nColumn);
                }

                string? line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] ss = line.Split('\t');
                    Helper.ParseLine(ss, gender, allNames, nameType, fColumn, nColumn);
                }
            }

            return allNames;

        }

        public StreamReader OpenReader(string folder, string file) //here we can either force encoding or turn bom detection on 
        {
            Encoding? forcedEncoding = _config.GetEncoding();

            if (forcedEncoding != null)
            {
                // Denmark: force Windows‑1252
                return new StreamReader(Path.Combine(folder, file), forcedEncoding, detectEncodingFromByteOrderMarks: false);
            }

            // Everyone else: BOM detection ON
            return new StreamReader(Path.Combine(folder, file), detectEncodingFromByteOrderMarks: true);
        }
    }
}