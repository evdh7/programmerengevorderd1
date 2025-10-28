using LegoTwo_BL.Interfaces;
using LegoTwo_DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegoTwo_Util
{
    public static class LegoTwoFileReaderFactory
    {
        public static ILegoTwoFileReader GiveFileReader()
        {
            return new FileReader();
        }
    }
}
