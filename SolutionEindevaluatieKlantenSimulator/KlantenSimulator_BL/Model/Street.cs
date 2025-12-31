using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Model
{
    public class Street(string street, int cityId)
    {
        public string StreetName { get; set; } = street;
        public int CityId { get; set; } = cityId;

    }
}
