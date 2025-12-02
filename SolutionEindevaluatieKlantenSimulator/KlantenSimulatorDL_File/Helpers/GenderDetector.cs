namespace KlantenSimulatorDL_File.Helpers
{
    namespace KlantenSimulatorDL_File.Helpers
    {
        public static class GenderDetector
        {
            public static string? GetGender(string sectionName)
            {
                switch (sectionName)
                {
                    case "MaleNames": return "M";
                    case "FemaleNames": return "F";
                    default: return null;
                }
            }
        }
    }
}
