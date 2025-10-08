using KlantenSimulatorBL.Interfaces;
using System.Runtime.CompilerServices;

namespace KlantenSimulatorDL
{
    public class BestandsLezer: IBestandsLezer
    {
        public List<string> LeesNamen(string pad)
        {
            List<string> namen = new List<string>();
            using (StreamReader sr = new StreamReader(pad))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    namen.Add(line.Split(';')[1].Trim());
                }
            }
            return namen;
        }

        public List<(int postcode,string gemeente)> LeesPostcodeGemeente (string pad)
        {
            List<(int,string)> postcodeGemeente = new();
            using (StreamReader sr = new StreamReader(pad))
            {
                string line;
                sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    string[]ss= line.Split(";");
                    if (!string.IsNullOrWhiteSpace(ss[0]))
                    {
                        postcodeGemeente.Add((int.Parse(ss[0].Trim()), ss[1].Trim()));
                    }

                }
            }
            return postcodeGemeente;
        }
        public List<string> LeesStraten(string pad)
        {
            HashSet<string> namen = new();
            using (StreamReader sr = new StreamReader(pad))
            {
                string line;
                sr.ReadLine();
                while ((line = sr.ReadLine()) != null)
                {
                    namen.Add(line.Split(',')[2].Trim());

                }
            }
            return namen.ToList();
        }
    }
}
