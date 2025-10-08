using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorBL.Model;

namespace KlantenSimulatorDL
{
    public class BestandSchrijver: IBestandsSchrijver
    {
        public void SchrijfBestand(List<Persoon> data, string pad)
        {
            using (StreamWriter sw = new StreamWriter(pad))
                foreach (Persoon p in data)
                {
                    sw.WriteLine(p.ToString());
                }
        }
    }
}
