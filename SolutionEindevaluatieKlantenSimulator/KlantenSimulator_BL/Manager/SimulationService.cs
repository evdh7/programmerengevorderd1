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

        public List<City> GetCities(string countryName)
        {
            return repo.GetCities(countryName);
        }

        public List<string> GetCountries()
        {
            return repo.GetCountries();
        }
        public IEnumerable<Dataset> GetDataset(string countryName)
        {
            return repo.GetDataSet(countryName);
        }


        public void StartSimulation(SimulationParameters parameters)
        {
            repo.StartSimulation(parameters);
        }


    }
}
