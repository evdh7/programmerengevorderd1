using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegoTwo_BL.Interfaces;

namespace LegoTwo_BL.Beheer
{
    public class LegoBeheerder
    {
        private ILegoTwoFileReader fileReader;
        private ILegoTwoRepository repo;

        public LegoBeheerder (ILegoTwoFileReader fileReader, ILegoTwoRepository repo)
        {
            this.fileReader = fileReader;
            this.repo = repo;
        }

        public void UploadNaarDataBank(string bestand)
        {
            var data = fileReader.ReadFile(bestand);
            repo.WriteLegoThemes(data);
        }
    }
}
