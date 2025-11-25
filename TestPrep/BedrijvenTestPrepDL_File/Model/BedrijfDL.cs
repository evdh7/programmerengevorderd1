using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrijvenTestPrepDL_File.Model
{
    internal class BedrijfDL
    {
        public string Name { get; set; }
        public string Industrie { get; set; }
        public string Sector { get; set; }
        public string Location { get; set; }
        public int Year { get; set; }
        public string Info { get; set; }

        public List <PersoonDL> Personeelsleden { get; set; } = new ();

    }
}
