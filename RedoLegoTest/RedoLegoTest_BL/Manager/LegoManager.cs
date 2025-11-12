using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedoLegoTest_BL.Interfaces;

namespace RedoLegoTest_BL.Manager
{
    public class LegoManager
    {
        private IFileReader reader;
        private ILegoRepository legoRepository;

        public LegoManager(IFileReader reader, ILegoRepository legoRepository)
        {
            this.reader = reader;
            this.legoRepository = legoRepository;
        }

        public void DBUpload(string path)
        {
            var data = reader.ReadFile(path); 
            legoRepository.WriteLegoThemes(data);
        }
    }
}
