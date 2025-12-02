using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDL_SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorUtils
{
    public static class KlantenSimulatorSQLFactory
    {
        public static IFileRepository GetRepository(string connectionString)
        {
            return new KlantenSimulatorRepository(connectionString);
        }
    }
}
