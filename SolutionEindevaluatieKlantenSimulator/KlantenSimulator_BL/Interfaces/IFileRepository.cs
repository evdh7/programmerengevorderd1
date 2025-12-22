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
            void InsertName(List<NameDTO.NameEntry> names, int datasetId);
            int InsertAddress(CountryDTO entry);

    }
}
