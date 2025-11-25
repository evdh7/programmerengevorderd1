using BedrijvenTestPrepBL.Interfaces;
using BedrijvenTestPrepDL_File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrijvenTestPrepUtil
{
    public static class BedrijfBestandslezerFactory
    {
        public static IBedrijfBestandslezer GeefBestandslezer()
        {
            return new BedrijfBestandsLezer();
        }
    }
}
