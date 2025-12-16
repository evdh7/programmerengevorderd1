using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VakantieparkBL.Interfaces;
using VakantieparkDL;

namespace VakantieparkUtils
{
    public class VakantieparkRepositoryFactory
    {
        public static IVakantieparkRepository GetVakantieRepository()
        {
            return new VakantieparkRepository();
        }
    }
}
