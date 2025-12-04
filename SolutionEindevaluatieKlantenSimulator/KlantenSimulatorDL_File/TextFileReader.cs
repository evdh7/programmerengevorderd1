using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDL_File.Helpers.KlantenSimulatorDL_File.Helpers;

namespace KlantenSimulatorDL_File
{
    public class TextFileReader : IFileReader
    {
        public List<FirstNameDTO> ReadFirstNames(string folder, List<string> fileNames)
        {
            List<FirstNameDTO> firstNames = new List<FirstNameDTO>();
            var nameFiles = fileNames
                .Where(f => f.ToLower().Contains("male") || f.ToLower().Contains("female"))
                .ToList();

            foreach (var file in nameFiles)
            {
                using (StreamReader sr = new StreamReader(Path.Combine(folder, file)))
                {
                    Gender gender = Helper.GetGender(file);
                    string line;
                    int skipLines = Helper.SkipLines(file);

                    for (int i = 0; i < skipLines && !sr.EndOfStream; i++)
                    {
                        line = sr.ReadLine();
                    }

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] ss = line.Split('\t');
                        string name = "";
                        int frequency;

                        if (int.TryParse(ss[0], out int value))
                        {
                            name = ss[1];
                            frequency = int.Parse(ss[2]);
                        }
                        name = ss[0];
                        frequency = int.Parse(ss[1]);

                        firstNames.Add(new FirstNameDTO(name, gender, frequency));
                    }
                }
            }
            return firstNames;

        }

        public List<LastNameDTO> ReadLastNames(string folder, List<string> fileNames)
        {
            List<LastNameDTO> lastNames = new List<LastNameDTO>();

            using (StreamReader sr = new StreamReader(Path.Combine(folder, fileNames[1])))
            {
                string line;
                int skipLines = Helper.SkipLines(fileNames[1]);

                for (int i = 0; i < skipLines && !sr.EndOfStream; i++)
                {
                    line = sr.ReadLine();
                }

                while ((line = sr.ReadLine()) != null)
                {
                    string[] ss = line.Split('\t');
                    string name = "";
                    int frequency;

                    if (int.TryParse(ss[0], out int value))
                    {
                        name = ss[1];
                        frequency = int.Parse(ss[2]);
                    }
                    name = ss[0];
                    frequency = int.Parse(ss[1]);

                    lastNames.Add(new LastNameDTO(name, frequency));
                }
                return lastNames;
            }
        }

        CountryDTO IFileReader.ReadAddresses(string folder, List<string> fileNames, string country)
        {
            throw new NotImplementedException();
        }
    }
}
