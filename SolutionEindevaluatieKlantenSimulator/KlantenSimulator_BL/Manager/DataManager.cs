using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorBL.Enums;
using System.Data;
using KlantenSimulatorBL.Model;

namespace KlantenSimulatorBL.Manager
{
    public class DataManager(IFileRepository repo)
    {
        private readonly IFileRepository repo = repo;

        public int UploadAddresses(IAddressReader addressReader, string folder, string fileName, string countryName)
        {
            var country = addressReader.ReadAddresses(folder, fileName, countryName);
            int datasetId = repo.InsertAddress(country);
            return datasetId;
        }
        public void UploadNames(INameReader nameReader, string folder, (string Key, string? Value)[]files, int datasetId, NameType type, Gender? gender)
        {
            var names = nameReader.ReadNames(folder, files, type, gender);
            repo.InsertName(names, datasetId);
        }

        


    }

    }


