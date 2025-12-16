using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VakantieparkBL.Model;

namespace VakantieparkBL.Interfaces
{
    public interface IVakantieparkRepository
    {
        List<Contactpersoon> GeefContacten();
        List<Faciliteit> GeefFaciliteiten();
        List<VakantieparkDTO> GeefVakantieparken();
        List<Wooneenheid> GeefWooneenheden(int vakantieparkID);
        void VoegFaciliteitToe(Faciliteit faciliteit);
    }
}
