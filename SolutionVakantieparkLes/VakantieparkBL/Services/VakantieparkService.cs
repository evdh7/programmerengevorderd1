using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VakantieparkBL.DTOs;
using VakantieparkBL.Interfaces;

namespace VakantieparkBL.Services
{
    public class VakantieparkService
    {

        private IVakantieparkRepository repo;

        public VakantieparkService(IVakantieparkRepository repo)
        {
            this.repo = repo;
        }

        public List<VakantieparkDTO> GeefVakantieparken()
        {
            return repo.GeefVakantieparken();
        }
    }
}
