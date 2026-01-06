using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorBL.Model;

namespace KlantenSimulatorBL.Manager
{
    public class SimulationService(IFileRepository repo)
    {
        private readonly IFileRepository repo = repo;

        public static void AddClient(Client client)
        {
            //    repo.AddCustomer(customer);

        }

        public List<CityDTO> GetCities(string countryName)
        {
            return repo.GetCities(countryName);
        }

        public Dictionary<int,string> GetCountries()
        {
            return repo.GetCountries();
        }
        public IEnumerable<Dataset> GetDataset(string countryName)
        {
            return repo.GetDataSet(countryName);
        }
        public List<Person> StartSimulation(SimulationParameters parameters)
        {
            return repo.StartSimulation(parameters);
        }


    }
}
