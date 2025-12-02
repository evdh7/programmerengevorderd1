using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vakantiepark2DL.TO;

namespace Vakantiepark2DL.DAOs
{
    public class VakantieparkDAO
    {
        private Dictionary<int, VakantieparkTO> vakantieparken = new();
        private int vakantieparkId = 1;
        public VakantieparkDAO()
        {
            vakantieparken.Add(vakantieparkId, new VakantieparkTO(vakantieparkId, "Gentpark", "Gent", contactpersonen[1], new List<FaciliteitTO>() { faciliteiten[1], faciliteiten[2], faciliteiten[3] }, wooneenheden.Values.Where(x => x.Adres.StartsWith("hoofd")).ToList())); vakantieparkId++;
            vakantieparken.Add(vakantieparkId, new VakantieparkTO(vakantieparkId, "Lokeren Meersen", "Lokeren", contactpersonen[2], new List<FaciliteitTO>() { faciliteiten[1], faciliteiten[2], faciliteiten[3] }, wooneenheden.Values.Where(x => x.Adres.StartsWith("speel")).ToList())); vakantieparkId++;
        }
    }
}
