using KlantenSimulatorBL.Enums;

namespace KlantenSimulatorDL_File.Helpers
{
    namespace KlantenSimulatorDL_File.Helpers
    {
        public static class Helper
        {
            public static Gender GetGender(string sectionName)
            {
                string lowerCase = sectionName.ToLower();

                if (lowerCase.Contains("male"))
                    return Gender.Male;
                if (lowerCase.Contains("female"))
                    return Gender.Female;

                return Gender.Unknown;


            }

            public static int SkipLines(string file)
            {
                using (StreamReader sr = new StreamReader(file))
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

                        if (ss.Count() >= 2 && !string.IsNullOrWhiteSpace(ss[1]))
                            break;
                        skipped++;
                    }

                    return skipped;


                }
            }
        }
    }
}

