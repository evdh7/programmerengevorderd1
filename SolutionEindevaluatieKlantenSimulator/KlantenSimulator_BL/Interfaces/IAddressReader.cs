using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Model;
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
        List<NameDTO.NameEntry> ReadNames(string folder, (string, string)[] fileNames, NameType nameType, Gender? gender);
    }
}
