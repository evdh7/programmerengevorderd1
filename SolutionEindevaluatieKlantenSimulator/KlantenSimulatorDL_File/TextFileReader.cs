using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDL_File.Helpers.KlantenSimulatorDL_File.Helpers;
using System.Globalization;

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
                int skipLines = Helper.SkipLines(folder, fileName);

                for (int i = 0; i < skipLines && !sr.EndOfStream; i++)
                {
                    line = sr.ReadLine();
                }

                while ((line = sr.ReadLine()) != null)
                {
                    string[] ss = line.Split('\t');
                    string name = "";
                    double frequency;

                    if (int.TryParse(ss[0], out int value))
                    {
                        name = ss[1];
                        frequency = double.TryParse(ss[2], System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out double x) ? x : 0;
                    }
                    else
                    {
                        name = ss[0];
                        frequency = double.TryParse(ss[1], System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out double y) ? y : 0;
                    }
                    names.Add(new NameDTO(name, gender, frequency));
                }
                result.Add(nameType, names);
            }

            return result;
        }
    }
}
