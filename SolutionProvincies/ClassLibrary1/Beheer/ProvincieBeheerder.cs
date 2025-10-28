using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProvinciesBL.Interfaces;

namespace ProvinciesBL.Beheerders
{
    public class ProvincieBeheerder
    {
        private IProvincieRepository rep;
        private IProvincieBestandslezer bestandslezer;

        public ProvincieBeheerder(IProvincieRepository rep, IProvincieBestandslezer bestandslezer)
        {
            this.rep = rep;
            this.bestandslezer = bestandslezer;
        }

        public void UploadNaarDatabank(string folder, List<string> bestandsnamen)
        {
            //stap 1 lezen bestanden
            var data = bestandslezer.LeesBestanden(folder, bestandsnamen);
            //stap 2 schrijven naar databank
            rep.UploadToDatabase(data);
        }
    }
}
