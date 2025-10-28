using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProvinciesBL.Interfaces;
using ProvinciesBL.Model;
using ProvinciesDL_SQL;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvinciesUtil
{
    public static class ProvincieRepositoryFactory
    {
        public static IProvincieRepository GeefRepository(string connectionString)
        {
            return new ProvincieRepository(connectionString);
        }

        
    }
}
