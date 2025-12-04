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

        public void UploadToDatabase(string folder, List<string> fileNames, string countryName)
        {
            // Stap 1: lees data uit bestanden
            var firstNames = fileReader.ReadFirstNames(folder, fileNames, countryName);
            var lastNames = fileReader.ReadLastNames(folder, fileNames, countryName);
            var country = fileReader.ReadAddresses(folder, fileNames, countryName);

            // Stap 2: upload naar database
            repo.InsertAddress(country);

            repo.InsertFirstName(firstNames);

            repo.InsertLastName(lastNames);

        }

    }
}

//TO DO rewrite with folder and sections for each countryName and add gender