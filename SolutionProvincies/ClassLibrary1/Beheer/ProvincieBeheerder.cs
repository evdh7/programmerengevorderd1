using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProvinciesBL.Interfaces;
using ProvinciesBL.Model;

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
        public List<string> GeefInhoudZip(string fileName)
        {
            return bestandslezer.GeefInhoudZip(fileName);
        }

        public Statistieken UploadNaarDatabank(string folder, List<string> bestandsnamen)
        {
            //stap 1 lezen bestanden
            var data = bestandslezer.LeesBestanden(folder, bestandsnamen);
            //stap 2 schrijven naar databank
            rep.UploadToDatabase(data);

            return new Statistieken(data);

        }

        public void ClearFolder(string folderName)
        {
            bestandslezer.ClearFolder(folderName);
        }
        public bool IsFolderEmpty(string folderName)
        { 
            return bestandslezer.IsFolderEmpty(folderName);
        }

        public void Unzip(string zipFile, string outputFolder)
        {
            bestandslezer.Unzip(zipFile, outputFolder);
        }




    }
}
