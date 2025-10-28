using LegoTwo_BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegoTwo_BL.Interfaces
{
    public interface ILegoTwoFileReader
    {
        List<LegoTheme> ReadFile(string path);
    }
}
