using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Exceptions;
using System.Globalization;
using System.Text;

namespace KlantenSimulatorDL_File.Helpers
{
    namespace KlantenSimulatorDL_File.Helpers
    {
        public static class Helper
        {
          
            public static int FindFrequencyColumn(string[] ss)
            {
                int fColumn = 0;

                bool success = IsThisAnInteger(ss[0]);

                if (success == true) //Spain uses decimal numbers in their first column so fix that i guess
                {
                    fColumn = 2;
                }

                else
                {
                    fColumn++;
                }
                return fColumn;
            }

            public static bool IsThisAnInteger(string number)
            {
                bool success;

                string trimmed = number.Trim();
                if (trimmed.Contains("."))
                {
                    trimmed = trimmed.Replace(".", "");
                }

                success = int.TryParse(trimmed, CultureInfo.InvariantCulture, out int result);

                return success;
            }

            public static NameType GetNameType(string sectionName)
            {
                string lowerCase = sectionName.ToLower();

                if (lowerCase.Contains("first"))
                    return NameType.First;
                else if (lowerCase.Contains("last"))
                    return NameType.Last;
                else 
                    return NameType.FirstLast;
            }
            public static Gender GetGender(string sectionName)
            {
                string lowerCase = sectionName.ToLower();

                if (lowerCase.Contains("female"))
                    return Gender.Female;
                else 
                    return Gender.Male;


            }

            public static (int skipped, int frequency) SkipLines(string folder, string file)
            {
                using (StreamReader sr = new StreamReader(Path.Combine(folder, file)))
                {
                    string line;
                    int skipped = 0;
                    int fColumn = 0; ;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            skipped++;
                            continue;
                        }
                        string[] ss = line.Split('\t');

                        bool hasNumber = ss.Any(cell => int.TryParse(cell.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out _));

                        if (!hasNumber)
                        {
                            skipped++;
                            continue;
                        }

                        fColumn = FindFrequencyColumn(ss);


                        if (fColumn != 0)
                        {
                            break;
                        }
                        skipped++;
                    }

                    return (skipped, fColumn);


                }
            }

            public static string ExtractCityName(string input, string searchString)
            {
                string cityName = "";
                if (input.ToLower().EndsWith(searchString))
                {
                    int startIndex = 0; //the string we wants starts at position 0
                    int endIndex = input.ToLower().IndexOf(searchString);
                    return cityName = input.Substring(startIndex, endIndex).Trim(); //we want everything right before the whitespace before the searchString
                }

                return input;
            }
            

        }

    }
}


