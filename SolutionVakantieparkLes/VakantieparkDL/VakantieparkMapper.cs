using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VakantieparkBL.DTOs;
using VakantieparkBL.Model;
using VakantieparkDL;

namespace VakantieparkDL
{
    internal static class VakantieparkMapper
    {
        public static VakantieparkDTO MapUitDomein(Vakantiepark v)
        {
            return new VakantieparkDTO((int)v.Id, v.Naam, v.Locatie, v.Capaciteit(), v.Wooneenheden.Count, v.Faciliteiten.Count, v.Contactpersoon.Email);
        }
    }
}
