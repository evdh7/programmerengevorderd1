using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Model
{
    public class Adres
    {
        public Adres(string gemeente, int postcode, string straat, string huisnummer)
        {
            Gemeente = gemeente;
            Postcode = postcode;
            Straat = straat;
            Huisnummer = huisnummer;
            
        }

        public string Gemeente {  get; set; }
        public int Postcode { get; set; }
        public string Straat { get; set; }
        public string Huisnummer { get; set; }
        public override string ToString()
        {
            return $"{Postcode}, {Gemeente}, {Straat}, {Huisnummer}";
        }
    }
}
