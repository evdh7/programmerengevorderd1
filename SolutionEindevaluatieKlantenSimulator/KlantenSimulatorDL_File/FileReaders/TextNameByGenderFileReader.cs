using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDL_File.Helpers.KlantenSimulatorDL_File.Helpers;

namespace KlantenSimulatorDL_File.FileReaders
{
    public class TextNameByGenderFileReader(INameReaderConfig config) : INameReader
    {
        private readonly INameReaderConfig _config = config;
        public List<NameDTO.NameEntry> ReadNames(string folder, (string, string)[] fileNames, NameType nameType, Gender? gender)
        {
            List<NameDTO.NameEntry> allNames = [];

            foreach (var file in fileNames)
            {

                using StreamReader sr = new(Path.Combine(folder, file.Item2));
                string[]? firstValidLine = Helper.SkipLines(sr);

                if (firstValidLine != null)
                {
                    var entries = FindNameFrequencyAndType(firstValidLine, nameType);

                    foreach (var (nameFrequency, g) in entries)
                    {
                        _config.SetFrequencyColumn((uint)nameFrequency);
                        uint fColumn = _config.GetFrequencyColumn();
                        uint nColumn = _config.GetNameColumn();
                        Helper.ParseLine(firstValidLine, g, allNames, nameType, fColumn, nColumn);
                    }
                }

                string? line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] ss = line.Split('\t');

                    var entries = FindNameFrequencyAndType(ss, nameType);
                    foreach (var (nameFrequency, g) in entries)
                    {
                        _config.SetFrequencyColumn((uint)nameFrequency);
                        uint fColumn = _config.GetFrequencyColumn();
                        uint nColumn = _config.GetNameColumn();
                        Helper.ParseLine(ss, g, allNames, nameType, fColumn, nColumn);
                    }
                }

            }
            return allNames;


        }

        private static List<(uint frequency, Gender?)> FindNameFrequencyAndType(string[] ss, NameType type) //a method for the Swiss file: depending on nametype the frequency column and gender can be different. we call the method once on the firstvalidline.
        {
            var results = new List<(uint, Gender?)>();

            if (type == NameType.Last)
            {
                results.Add((2, null)); //if the nametype of the array is last, the frequency is always column with index 2 and the gender is null
                return results;
            }
            else
            {
                if (uint.TryParse(ss[1], out uint valueF)) //if the value in column with index 2 is a uint, that value is the frequency of gender female for that name
                    results.Add((1, Gender.Female));
                else if (uint.TryParse(ss[2], out uint valueM))
                    results.Add((2, Gender.Male));
            }
            return results;
        }

    }

}
