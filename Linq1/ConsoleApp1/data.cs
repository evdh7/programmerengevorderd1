using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdressenOef
{
    public class Data
    {

        public string provincie;
        public string gemeente;
        public string straatnaam;

        public Data(string provincie, string gemeente, string straatnaam)
        {
            this.provincie = provincie;
            this.gemeente = gemeente;
            this.straatnaam = straatnaam;
        }
        public override string ToString()
        {
            return $"{provincie}{gemeente}{straatnaam}";
        }
    }
}