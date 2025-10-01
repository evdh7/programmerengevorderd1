namespace Linq
{
    public class AdresInfo
    {
        private string source;
        private List<Data> adressen;
        public AdresInfo(string source)
        {
            this.source = source;
            adressen = new List<Data>();
        }

        public void ReadData()
        {
            using StreamReader bestandLezen = new StreamReader(@"C:\Users\emmy\OneDrive - Hogeschool Gent\Documents\Programmeren Gevorderd 1\adresInfo\adresInfo.txt"); string line; while ((line = bestandLezen.ReadLine()) != null)
            {
                string[] x = line.Trim().Split(',');
                adressen.Add(new Data(x[0], x[1], x[2]));
            }
        }

        public List<string> GetProvincies()
        {
            return adressen.Select(a => a.Provincie).Distinct().ToList();
        }

        public List<string> GetStraatnamen()
        {
            return adressen.Select(a => a.Straatnaam).Distinct().ToList();
        }
        
    }
}