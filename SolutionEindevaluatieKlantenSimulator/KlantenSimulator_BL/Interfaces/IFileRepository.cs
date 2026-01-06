using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Model;
using static KlantenSimulatorBL.DTOs.NameDTO;

namespace KlantenSimulatorBL.Interfaces
{
    public interface IFileRepository
    {
        void InsertName(List<NameEntry> names, int datasetId);
        int InsertAddress(CountryDTO entry);
        Dictionary<int, string> GetCountries();
        List<CityDTO> GetCities(string countryName);
        IEnumerable<Dataset> GetDataSet(string countryName);
        List<Person> StartSimulation(SimulationParameters parameters);

    }
}
