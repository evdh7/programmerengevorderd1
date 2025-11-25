using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityDL.Model;

namespace CityDL.DAOs
{
    public class AttractionDAO
    {

        private string connectionString;

        public AttractionDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public AttractionTO Save(AttractionTO attraction)
        {
            return null;
        }
    }
}
