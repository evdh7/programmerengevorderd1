using CityDL.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityDL.DAOs
{
    public class CityDAO
    {

        private string connectionString;

        public CityDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public CityTO Save(CityTO cityTO)
        {
            return null;
        }
        public List<CityTO> GetAll()
        {
            return null;
        }
        public CityTO GetById(int id)
        {
            string SQL = "select * from city where id=@id";
            using (SqlConnection con = new SqlConnection(connectionString)) {
        }
    }
}
