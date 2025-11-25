using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BedrijvenTestPrepBL.Model;

namespace BedrijvenTestPrepBL.Interfaces
{
    public interface IBedrijfBestandslezer
    {
        List<Bedrijf> ReadFile(string path, string logPath);
    }
}
