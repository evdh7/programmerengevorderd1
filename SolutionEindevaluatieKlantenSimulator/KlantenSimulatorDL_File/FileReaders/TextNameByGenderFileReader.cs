using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDL_File.Helpers.KlantenSimulatorDL_File.Helpers;
using static KlantenSimulatorBL.DTOs.NameDTO;

namespace KlantenSimulatorDL_File.FileReaders
{
    public class TextNameByGenderFileReader : INameReader
    {
        public List<NameEntry> ReadNames(string folder, (string, string)[] fileNames, NameType nameType, Gender? gender)
        {
            List<NameEntry> allNames = new List<NameEntry>();

            foreach (var file in fileNames)
            {

                using (StreamReader sr = new StreamReader(Path.Combine(folder, file.Item2)))
                {
                    (_, string[] firstValidLine) = Helper.SkipLines(sr);


                    if (firstValidLine != null)
                    {
                        var entries = FindNameFrequencyAndType(firstValidLine, nameType);
                        foreach (var (nameFrequency, g) in entries)
                        {
                            Helper.ParseLine(firstValidLine, nameFrequency, g, allNames, nameType);
                        }
                    }

                    string? line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] ss = line.Split('\t');

                        var entries = FindNameFrequencyAndType(ss, nameType);
                        foreach(var (nameFrequency, g) in entries) 
                        {
                            Helper.ParseLine(ss, nameFrequency, g, allNames, nameType);
                        }
                    }

                }

            }
            return allNames;


        }

        private static List<(int frequency, Gender?)> FindNameFrequencyAndType(string[] ss, NameType type)
        {
            var results = new List<(int, Gender?)>();

            if (type == NameType.Last)
            {
                results.Add((2, null));
                return results;
            }
            if (int.TryParse(ss[1], out int valueF))
                results.Add((1, Gender.Female));
            if (int.TryParse(ss[2], out int valueM))

                results.Add((2, Gender.Male));
            return results;
        }

}

}
