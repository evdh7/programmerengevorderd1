using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VakantieparkBL.Interfaces;
using VakantieparkBL.Model;

namespace VakantieparkBL.Services
{
    public class VakantieparkService
    {
        private IVakantieparkRepository repo;

        public VakantieparkService(IVakantieparkRepository repo)
        {
            this.repo = repo;
        }

        public List<Contactpersoon> GeefContacten()
        {
            return repo.GeefContacten();
        }

        public List<Faciliteit> GeefFaciliteiten()
        {
            return repo.GeefFaciliteiten();
        }

        public List<VakantieparkDTO> GeefVakantieparken()
        {
            return repo.GeefVakantieparken();
        }

        public void VoegFaciliteitToe(Faciliteit faciliteit)
        {
            repo.VoegFaciliteitToe(faciliteit);
        }
        public List<Wooneenheid> GeefWooneenheden(int vakantieparkID)
        {
            return repo.GeefWooneenheden(vakantieparkID);
        }
    }
}
