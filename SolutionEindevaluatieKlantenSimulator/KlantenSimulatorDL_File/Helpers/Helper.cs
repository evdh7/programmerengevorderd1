using KlantenSimulatorBL.Enums;

namespace KlantenSimulatorDL_File.Helpers
{
    namespace KlantenSimulatorDL_File.Helpers
    {
        public static class Helper
        {
            public static string? GetGender(string sectionName)
            {
                string lowerCase = sectionName.ToLower();

                if (lowerCase.Contains("male"))
                    return Gender.Male;
                if (lowerCase.Contains("female"))
                    return Gender.Female;

                return Gender.Unknown;

                }
            }
        }
    }
}
