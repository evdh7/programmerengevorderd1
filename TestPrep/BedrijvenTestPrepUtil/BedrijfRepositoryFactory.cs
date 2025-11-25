using BedrijvenTestPrepBL.Interfaces;
using BedrijvenTestPrepDL_SQL;

namespace BedrijvenTestPrepUtil
{
    public static class BedrijfRepositoryFactory
    {
        public static IBedrijfRepository GeefRepository(string connectionString)
        {
            return new BedrijfRepository(connectionString);
        }
    }
}
