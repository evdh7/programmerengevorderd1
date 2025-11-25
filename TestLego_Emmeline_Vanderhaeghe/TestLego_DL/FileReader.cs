using TestLego_BL.Interfaces;

public class FileReader : ILegoFileReader
{
    public List<LegoTheme> ReadFile(string path)
    {
       // List<LegoTheme> legoThemes = new List<LegoTheme>();
        Dictionary<string, LegoTheme> data = new();
        using StreamReader sr = new StreamReader(path);
        {
            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                try { 

                string[] ss = line.Split('|');

                string s_id = ss[0];
                string s_name = ss[1];
                int s_year = int.Parse(ss[2]);
                int s_pieces = int.Parse(ss[7]);
                int s_minifigs = int.Parse(ss[8]);
                int? s_minage = int.Parse(ss[9]);
                string s_imageUrl = ss[6];
                double s_retailPrice = double.Parse(ss[13]);

                string t_theme = ss[3];


                
                    LegoSet legoSet = new LegoSet(s_id, s_name, s_year, s_pieces, s_minifigs, s_minage, s_imageUrl, s_retailPrice);

                    if (data.ContainsKey(t_theme))
                    {
                        data[t_theme].AddLegoSet(legoSet);
                    }
                    else
                    {
                        LegoTheme legoTheme = new LegoTheme(t_theme);
                        legoTheme.AddLegoSet(legoSet);
                        data.Add(t_theme, legoTheme);
                    }


                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return (data.Values.ToList());



        }

    }
}