using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Interfaces
{
    public interface IBestandsLezer
    {
        List<string> LeesNamen(string pad);
        List<(int postcode, string gemeente)> LeesPostcodeGemeente(string pad);
        List<string> LeesStraten(string pad);

    }
}
