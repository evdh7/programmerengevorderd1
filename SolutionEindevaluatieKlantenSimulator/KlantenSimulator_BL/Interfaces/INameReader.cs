using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Interfaces
{
    public interface INameReader
    {
        Dictionary<NameType, List<NameDTO>> ReadNames(string folder, string fileName, NameType nameType, Gender gender);

    }
}
