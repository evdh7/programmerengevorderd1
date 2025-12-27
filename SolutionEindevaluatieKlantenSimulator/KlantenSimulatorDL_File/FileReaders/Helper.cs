using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Exceptions;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDL_File.FileReaders;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using static KlantenSimulatorBL.DTOs.NameDTO;

namespace KlantenSimulatorDL_File.Helpers
{
    namespace KlantenSimulatorDL_File.Helpers
    {
        public class Helper
        {
            public static string?[] SkipLines(StreamReader sr)
            {
                string? line;
                int frequencyColumn = 0;

                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }
                    string[] ss = line.Split('\t');

                    bool hasNumber = ss.Any(cell => IsInteger(cell));

                    if (!hasNumber) continue;

                    return ss;
                }

                return null;
            }
            public static void ParseLine(string[] ss, Gender? gender, List<NameEntry> names, NameType nameType, INameReaderConfig layout)
            {
                uint fColumn = layout.GetFrequencyColumn();
                uint nColumn = layout.GetNameColumn();

                if (string.IsNullOrWhiteSpace(ss[0]))
                    return;

                if (ss.Length <= fColumn)
                    return;

                string trimmed = ss[fColumn].Trim().Replace(".", "");

                if (!int.TryParse(trimmed, NumberStyles.Integer, CultureInfo.InvariantCulture, out int frequency))
                    return;

                string name = ss[nColumn];

                names.Add(new NameEntry(name, nameType, gender, frequency));
            }
     
            private static bool IsInteger(string input)
            {
                string trimmed = input.Trim().Replace(".", "");

                return int.TryParse(trimmed, CultureInfo.InvariantCulture, out int result);
            }

            public static string ExtractCityName(string input, string searchString)
            {
                if (input.ToLower().EndsWith(searchString))
                {
                    int startIndex = 0; //the string we wants starts at position 0
                    int endIndex = input.IndexOf(searchString, StringComparison.CurrentCultureIgnoreCase);
                    return input.Substring(startIndex, endIndex).Trim(); //we want everything right before the whitespace before the searchString
                }

                return input;
            }
        }
    }
}


