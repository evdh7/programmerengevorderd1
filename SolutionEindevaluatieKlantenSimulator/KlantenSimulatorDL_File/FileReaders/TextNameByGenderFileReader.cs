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

                        Helper.ParseLine(firstValidLine, g, allNames, nameType, _config);
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

                        Helper.ParseLine(ss, g, allNames, nameType, _config);
                    }
                }

            }
            return allNames;


        }

        private static List<(uint frequency, Gender?)> FindNameFrequencyAndType(string[] ss, NameType type)
        {
            var results = new List<(uint, Gender?)>();

            if (type == NameType.Last)
            {
                results.Add((2, null));
                return results;
            }
            if (uint.TryParse(ss[1], out uint valueF))
                results.Add((1, Gender.Female));
            if (uint.TryParse(ss[2], out uint valueM))

                results.Add((2, Gender.Male));
            return results;
        }

}

}
