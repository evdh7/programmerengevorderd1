using KlantenSimulatorBL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Interfaces
{
    public interface IFileReader
    {

        List<FirstNameDTO>ReadFirstNames(string folder, List<string> fileNames, string country);
        List<LastNameDTO>ReadLastNames(string folder, List<string> fileNames, string country);
        List<AddressDTO>ReadAddresses(string folder, List<string> fileNames, string country);
    }
}
