using KlantenSimulatorBL.Interfaces;

namespace KlantenSimulatorBL.Manager
{
    public class DataManager
    {
        private IFileReader fileReader;
        private IFileRepository repo;

        public DataManager(IFileReader fileReader, IFileRepository repo)
        {
            this.fileReader = fileReader;
            this.repo = repo;
        }

        public void UploadToDatabase(string folder, List<string> fileNames, string country)
        {
            // Stap 1: lees data uit bestanden
            var firstNames = fileReader.ReadFirstNames(folder, fileNames, country);
            var lastNames = fileReader.ReadLastNames(folder, fileNames, country);
            var addresses = fileReader.ReadAddresses(folder, fileNames, country);

            // Stap 2: upload naar database
            foreach (var fn in firstNames)
                repo.InsertFirstName(fn);

            foreach (var ln in lastNames)
                repo.InsertLastName(ln);

            //foreach (var addr in addresses)
            //    repo.InsertAddress(addr);
        }

    }
}

//TO DO rewrite with folder and sections for each country and add gender