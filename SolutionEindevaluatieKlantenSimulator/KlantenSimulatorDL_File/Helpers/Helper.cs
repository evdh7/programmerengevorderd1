using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Exceptions;
using System.Globalization;

namespace KlantenSimulatorDL_File.Helpers
{
    namespace KlantenSimulatorDL_File.Helpers
    {
        public static class Helper
        {
            static bool TryParseFrequency(string line, out int frequency)
            {
                line = line.Replace(" ", "").Replace("'", "");
                if (line.Count(c => c == '.') >= 1 && !line.Contains(","))
                {
                    line = line.Replace(".", "");
                }
                if (line.Contains(",") && !line.Contains("."))
                {
                    line = line.Replace(",", ".");
                }

                frequency = 0;
                return double.TryParse(line, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double value);

            }

                
            
            public static NameType GetNameType(string sectionName)
            {
                string lowerCase = sectionName.ToLower();

                if (lowerCase.Contains("first"))
                    return NameType.First;
                else if (lowerCase.Contains("last"))
                    return NameType.Last;
                else
                    throw new KlantenSimulatorException($"No type available for {sectionName}");

            }
            public static Gender GetGender(string sectionName)
            {
                string lowerCase = sectionName.ToLower();

                if (lowerCase.Contains("female"))
                    return Gender.Female;
                if (lowerCase.Contains("male"))
                    return Gender.Male;

                return Gender.Unknown;

            }

            public static int SkipLines(string folder, string file)
            {
                using (StreamReader sr = new StreamReader(Path.Combine(folder, file)))
                {
                    string line;
                    int skipped = 0;

                    while ((line = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            skipped++;
                            continue;
                        }
                        string[] ss = line.Split('\t');

                        if (double.TryParse(ss[0], out double order))
                        {
                            if (ss.Count() >= 2)
                            {
                                string frequency = ss[2].Trim();

                                if (TryParseFrequency(frequency, out var freq))
                                {
                                    break;
                                }
                            }

                            skipped++;
                        }

                        else if (!double.TryParse(ss[0], out double value))
                        {
                            if (ss.Count() >= 2)
                            {
                                string frequency = ss[1].Trim();

                                if (TryParseFrequency(frequency, out var freq))
                                {
                                    break;
                                }
                            }

                            skipped++;
                        }
                    }
                    return skipped;

                }
            }
        }
    }
}

