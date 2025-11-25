using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VakantieparkBL.DTOs;

namespace VakantieparkBL.Interfaces
{
    public interface IVakantieparkRepository
    {
        List<VakantieparkDTO> GeefVakantieparken();
    }
}
