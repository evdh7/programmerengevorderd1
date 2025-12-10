using KlantenSimulatorBL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Interfaces
{
    public interface IAddressReader
    {
        CountryDTO ReadAddresses(string folder, string fileName, string country);

    }
}
