using LegoTwo_BL.Interfaces;
using LegoTwo_BL.Model;
using System.IO.Compression;

namespace LegoTwo_DL
{
    public class FileReader : ILegoTwoFileReader
    {
        public List<LegoTheme> ReadFile(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                sr.ReadLine();
                string line;
                Dictionary<string, LegoTheme> legoThemes = new();
                List<LegoSet> sets = new List<LegoSet>();
                List<LegoTheme> themes = new List<LegoTheme>();

                while ((line = sr.ReadLine()) != null)
                {
                    try
                    {

                        string[] ss = line.Split("|");

                        if (string.IsNullOrWhiteSpace(ss[7])) { continue; }

                        string s_id = ss[0];
                        string s_name = ss[1];
                        int.TryParse(ss[2], out int s_year);
                        int.TryParse(ss[7], out int s_pieces);
                        int s_minifigs = 0;
                        int.TryParse(ss[8], out s_minifigs);
                        int? s_minage = int.TryParse(ss[9], out var tempMinAge) ? tempMinAge : null;
                        string s_imageUrl = ss[6];
                        double? s_retailPrice = double.TryParse(ss[13], out var tempRetailPrice) ? tempRetailPrice : null;
                        string t_theme = ss[3];

                        LegoSet legoSet = new LegoSet
                        (
                            s_id, s_name, s_year, s_pieces, s_minifigs, s_minage, s_imageUrl, s_retailPrice
                        );

                        if (!legoThemes.ContainsKey(t_theme))
                        {
                            //LegoTheme legoTheme = new LegoTheme(t_theme);
                            legoThemes[t_theme] = new LegoTheme(t_theme);
                        }

                        legoThemes[t_theme].AddLegoSet(legoSet);

                    }
                    catch (Exception ex) { }




                }
                return legoThemes.Values.ToList();
            }
        }
    }
}