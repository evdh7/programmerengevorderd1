using RedoLegoTest_BL.Interfaces;
using RedoLegoTest_BL.Model;

namespace RedoLegoTestDL_File
{
    public class FileReader : IFileReader
    {
        public List<LegoTheme> ReadFile(string path)
        {
            Dictionary<string, LegoTheme> themes = new();

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    string[] ss = line.Split("|");
                    string id = ss[0];
                    string name = ss[1];
                    int year = int.Parse(ss[2]);
                    int pieces = string.IsNullOrWhiteSpace(ss[7]) ? 0 : int.Parse(ss[7]);
                    if (pieces == 0) continue;
                    int minifigs = string.IsNullOrWhiteSpace(ss[8]) ? 0 : int.Parse(ss[8]);
                    int? minage = string.IsNullOrWhiteSpace(ss[9]) ? null : int.Parse(ss[9]);
                    string imageUrl = ss[13];
                    double? retailPrice = string.IsNullOrWhiteSpace(ss[10]) ? null : double.Parse(ss[10]);
                    string theme = ss[3];
                    LegoSet legoSet = new LegoSet(id, name, year, pieces, minifigs, minage, imageUrl, retailPrice);

                    if (!themes.ContainsKey(theme))
                    {
                        //themes[theme] = new LegoTheme(t_theme);
                        themes.Add(theme, new LegoTheme(theme));
                    }

                    themes[theme].AddLegoSet(legoSet);
                }
            }
            return themes.Values.ToList();

        }
    }
}