using RedoLegoTest_BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedoLegoTest_BL.Interfaces
{
    public interface ILegoRepository
    {
        public void WriteLegoThemes(List<LegoTheme> themes);
    }
}
