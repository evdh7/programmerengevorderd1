using KlantenSimulatorBL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorBL.Interfaces
{
    public interface IFileRepository
    {
            void InsertFirstName(List <FirstNameDTO> entry, int datasetId);
            void InsertLastName(List <LastNameDTO> entry, int datasetId);
            int InsertAddress(CountryDTO entry);

    }
}
