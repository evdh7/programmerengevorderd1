using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Interfaces
{
    public interface IFileRepository
    {
            void InsertName(Dictionary<NameType, List <NameDTO>> entry, int datasetId);
            int InsertAddress(CountryDTO entry);

    }
}
