using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vakantiepark2BL.Model;
using Vakantiepark2DL.TO;

namespace Vakantiepark2DL.DAOs
{
    public class FaciliteitDAO
    {
        private Dictionary<int, FaciliteitTO> faciliteiten = new();
        private Dictionary<int, List<int>> tussentabel = new();
        private int faciliteitenId = 1;
        public FaciliteitDAO()
            {
            faciliteiten.Add(faciliteitenId, new FaciliteitTO(faciliteitenId, "speeltuin")); faciliteitenId++;
            faciliteiten.Add(faciliteitenId, new FaciliteitTO(faciliteitenId, "trampoline")); faciliteitenId++;

            faciliteiten.Add(faciliteitenId, new FaciliteitTO(faciliteitenId, "bowling")); faciliteitenId++;

            faciliteiten.Add(faciliteitenId, new FaciliteitTO(faciliteitenId, "tennis")); faciliteitenId++;

            faciliteiten.Add(faciliteitenId, new FaciliteitTO(faciliteitenId, "zwembad")); faciliteitenId++;

            tussentabel.Add(1, new List<int>() { 1, 4, 2 });
            tussentabel.Add(2, new List<int>() { 1, 3 });

        }
        public object GeefFaciliteitenVoorVakantiepark(int id)
        {
            List<int> fac_id;
            return faciliteiten.Values.Where(x=>x)
        }
    }
}
