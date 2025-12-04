using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDL_File.Helpers.KlantenSimulatorDL_File.Helpers;

namespace KlantenSimulatorDL_File
{
    public class TextNameByGenderFileReader : IFileReader
    {
        public List<FirstNameDTO> ReadFirstNames(string folder, List<string> fileNames)
        {
            List<FirstNameDTO> firstNames = new List<FirstNameDTO>();

            using (StreamReader sr = new StreamReader(Path.Combine(folder, fileNames[2])))
            {
                string? line;
                int skipLines = Helper.SkipLines(fileNames[2]);

                for (int i = 0; i < skipLines && !sr.EndOfStream; i++)
                {
                    line = sr.ReadLine();
                }

                while ((line = sr.ReadLine()) != null)
                {
                    string[] ss = line.Split('\t');
                    int frequency = 0;
                    Gender gender = Gender.Unknown;

                    string name = ss[0];

                    if (int.TryParse(ss[1], out int valueF))
                    {
                        frequency = int.Parse(ss[1]);
                        gender = Gender.Female;
                    }

                    else if (!int.TryParse(ss[1], out int value) && int.TryParse(ss[2], out int valueM))
                        frequency = int.Parse(ss[1]);
                    gender = Gender.Male;

                    firstNames.Add(new FirstNameDTO(name, gender, frequency));
                }
            }
            return firstNames;
        }

    

        public List<LastNameDTO> ReadLastNames(string folder, List<string> fileNames)
        {
            throw new NotImplementedException();
        }

        public CountryDTO ReadAddresses(string folder, List<string> fileNames, string country)
        {
            throw new NotImplementedException();
        }
    }

}
