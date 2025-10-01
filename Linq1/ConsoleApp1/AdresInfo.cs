using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Net.Sockets;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;

namespace AdressenOef
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
            string line;
            StreamReader reader = new StreamReader(source); 
            while ((line = reader.ReadLine()) != null)
            {
                string[] x = line.Trim().Split(',');
                adressen.Add(new Data(x[0], x[1], x[2]));
            }
        }

        public List<string> GetProvincies()
        {
            return adressen.Select(a => a.provincie).Distinct().ToList();
        }

        public List<string> GetStraatnamen(string gemeente)
        {
            return adressen.Select(a => a.straatnaam).Distinct().ToList();
        }
        public string LangsteStraatnaam()
        {
            return adressen.OrderByDescending(x => x.straatnaam.Length).Select(x => $"{x.straatnaam}, {x.gemeente}, {x.provincie}").First();
        }

        public string maxStraatnaam()
        {

            var x1 = adressen.GroupBy(x => x.gemeente).OrderByDescending(x => x.Count()).First();
            return x1.Key;
        }

    }
}