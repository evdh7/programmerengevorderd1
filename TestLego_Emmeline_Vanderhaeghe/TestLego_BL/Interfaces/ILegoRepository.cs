using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLego_BL.Interfaces
{
    public interface ILegoRepository
    {
        //public void GetLegoTheme(string setName);
        public void WriteLegoThemes(List<LegoTheme> data);
    }
}
