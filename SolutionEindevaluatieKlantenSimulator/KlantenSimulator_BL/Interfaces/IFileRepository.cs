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
            void InsertFirstName(FirstNameDTO entry);
            void InsertLastName(LastNameDTO entry);
            //void InsertAddress(AddressDTO entry);
        
    }
}
