using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Model;
using static KlantenSimulatorBL.DTOs.NameDTO;

namespace KlantenSimulatorBL.Interfaces
{
    public interface IFileRepository
    {
        void InsertName(List<NameEntry> names, int datasetId);
        int InsertAddress(CountryDTO entry);
        List<string> GetCountries();
        List<City> GetCities(string countryName);
        IEnumerable<Dataset> GetDataSet(string countryName);
        void StartSimulation(SimulationParameters parameters);
    }
}
