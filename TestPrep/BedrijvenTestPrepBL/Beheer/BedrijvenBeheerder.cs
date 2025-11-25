using BedrijvenTestPrepBL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrijvenTestPrepBL.Beheer
{
    public class BedrijvenBeheerder   
    {
        private IBedrijfBestandslezer bestandslezer;
        private IBedrijfRepository repo;

        public BedrijvenBeheerder(IBedrijfRepository repo, IBedrijfBestandslezer bestandslezer)
        {
            this.repo = repo;
            this.bestandslezer = bestandslezer;
        }

        public void UploadNaarDatabank(string path, string logPath)
        {
            var data = bestandslezer.ReadFile(path, logPath);
            repo.UploadToDatabase(data);
        }
    }
}
