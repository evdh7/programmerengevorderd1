using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;


namespace KlantenSimulatorDL_SQL
{
    public class KlantenSimulatorRepository : IFileRepository
    {
        private string connectionString;

        public KlantenSimulatorRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public void InsertAddress(AddressDTO entry)
        {
            string SQLaddress = "INSERT INTO address(country, city, street) VALUES(@country, @city, @street)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmdAddress = conn.CreateCommand())
            {
                conn.Open();
                SqlTransaction sqlTransaction = conn.BeginTransaction();
               
                    cmdAddress.Transaction = sqlTransaction;
                    cmdAddress.CommandText = SQLaddress;

                    cmdAddress.Parameters.Add(new SqlParameter("@country", SqlDbType.NVarChar));
                    cmdAddress.Parameters.Add(new SqlParameter("@city", SqlDbType.NVarChar));
                    cmdAddress.Parameters.Add(new SqlParameter("@street", SqlDbType.NVarChar));
                try
                {
                    cmdAddress.Parameters["@country"].Value = entry.Country;
                    cmdAddress.Parameters["@city"].Value = entry.City;
                    cmdAddress.Parameters["@street"].Value = entry.Street ?? (object)DBNull.Value;
                    cmdAddress.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    sqlTransaction.Rollback();
                }
            }
        }
        public void InsertFirstName(FirstNameDTO entry)
        {
            string SQLfirstName = "INSERT INTO first_name(name, gender, frequency, country) VALUES(@name, @gender, @frequency, @country)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmdFirstName = conn.CreateCommand())
            {
                conn.Open();
                SqlTransaction sqlTransaction = conn.BeginTransaction();
               
                    cmdFirstName.Transaction = sqlTransaction;
                    cmdFirstName.CommandText = SQLfirstName;

                    cmdFirstName.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar));
                    cmdFirstName.Parameters.Add(new SqlParameter("@gender", SqlDbType.Char));
                    cmdFirstName.Parameters.Add(new SqlParameter("@frequency", SqlDbType.Int));
                    cmdFirstName.Parameters.Add(new SqlParameter("@country", SqlDbType.NVarChar));
                try
                {
                    cmdFirstName.Parameters["@name"].Value = entry.Name;
                    cmdFirstName.Parameters["@gender"].Value = entry.Gender;
                    cmdFirstName.Parameters["@frequency"].Value = entry.Frequency ?? (object)DBNull.Value;
                    cmdFirstName.Parameters["@country"].Value = entry.Country;
                    cmdFirstName.ExecuteNonQuery();
                    sqlTransaction.Commit();
                }
                                    
            

                catch (Exception ex)
                {
                Console.WriteLine("Error: " + ex.Message);
                sqlTransaction.Rollback();
            }

        }
        }

        public void InsertLastName(LastNameDTO entry)
        {
            string SQLlastName = "INSERT INTO last_name(name, frequency, country) VALUES(@name, @frequency, @country)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmdLastName = conn.CreateCommand())
            {
                conn.Open();
                SqlTransaction sqlTransaction = conn.BeginTransaction();
                    cmdLastName.Transaction = sqlTransaction;
                    cmdLastName.CommandText = SQLlastName;

                    cmdLastName.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar));
                    cmdLastName.Parameters.Add(new SqlParameter("@frequency", SqlDbType.Int));
                    cmdLastName.Parameters.Add(new SqlParameter("@country", SqlDbType.NVarChar));
                try { 
                    cmdLastName.Parameters["@name"].Value = entry.LastName;
                    cmdLastName.Parameters["@frequency"].Value = entry.Frequency ?? (object)DBNull.Value;
                    cmdLastName.Parameters["@country"].Value = entry.Country;
                    cmdLastName.ExecuteNonQuery();
                    sqlTransaction.Commit();

                }

                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    sqlTransaction.Rollback();
                }
            }
        }
    }
}