using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDL_File.Helpers.KlantenSimulatorDL_File.Helpers;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KlantenSimulatorDL_File
{
    public class TextFileReader : INameReader
    {
        public Dictionary<NameType, List<NameDTO>> ReadNames(string folder, string fileName, NameType nameType, Gender gender)
        {
            Dictionary<NameType, List<NameDTO>> result = new Dictionary<NameType, List<NameDTO>> ();
            List<NameDTO> names = new List<NameDTO>();

            using (StreamReader sr = new StreamReader(Path.Combine(folder, fileName)))
            {

                string line;
                (int skipLines, int fColumn) = Helper.SkipLines(folder, fileName);

                for (int i = 0; i < skipLines && !sr.EndOfStream; i++)
                {
                    line = sr.ReadLine();
                }
                            
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] ss = line.Split('\t');

                    if (ss.Length <= fColumn)
                        continue;

                    string trimmed = ss[fColumn].Trim();
                    
                    if (string.IsNullOrEmpty(trimmed))
                        continue;

                    if (trimmed.Contains("."))
                    {
                        trimmed = trimmed.Replace(".", "");
                    }
                    
                    if (!int.TryParse(trimmed, NumberStyles.Integer, CultureInfo.InvariantCulture, out int frequency))
                    {
                        continue;
                    }

                    string name = ss[fColumn - 1];

                    names.Add(new NameDTO(name, gender, frequency));
                }
                result.Add(nameType, names);
            }

            return result;
        }
    }
}
