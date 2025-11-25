using TestLego_BL.Interfaces;

namespace LegoRepositoryFactpry
{
    public static class LegoRepositoryFactory
    {

        public static ILegoRepository GeefRepository(string connectionString)
        {
            return new LegoRepository(connectionString);
        }
    }
}


