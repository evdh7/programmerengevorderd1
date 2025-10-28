using ProvinciesBL.Interfaces;
using ProvinciesDL_File;

namespace ProvinciesUtil
{
    public static class ProvincieBestandslezerFactory
    {
        public static IProvincieBestandslezer GeefBestandslezer()
        {
            return new ProvincieBestandslezer();

        }
    }
}
