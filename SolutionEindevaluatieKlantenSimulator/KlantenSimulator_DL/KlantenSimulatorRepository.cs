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

        public void InsertAddress(CountryDTO data)
        {
            string SQLcountry = "INSERT INTO country(name) OUTPUT INSERTED.ID VALUES(@name)";
            string SQLdataset = "INSERT INTO dataset(country_id, year, description, date_imported) OUTPUT INSERTED.ID VALUES(@country_id,@year,@description,@date_imported)";
            string SQLcity = "INSERT INTO city(name, country_id) OUTPUT INSERTED.ID VALUES(@name, @country_id)";
            string SQLaddress = "INSERT INTO address(city_id, street, dataset_id) VALUES(@city_id, @street, @dataset_id)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmdCountry = conn.CreateCommand())
            using (SqlCommand cmdCity = conn.CreateCommand())
            using (SqlCommand cmdDataset = conn.CreateCommand())
            using (SqlCommand cmdAddress = conn.CreateCommand())
            {
                conn.Open();
                using (SqlTransaction sqlTransaction = conn.BeginTransaction())
                {

                    cmdCountry.Transaction = sqlTransaction;
                    cmdDataset.Transaction = sqlTransaction;
                    cmdCity.Transaction = sqlTransaction;
                    cmdAddress.Transaction = sqlTransaction;
                    cmdCountry.CommandText = SQLcountry;
                    cmdDataset.CommandText = SQLdataset;
                    cmdCity.CommandText = SQLcity;
                    cmdAddress.CommandText = SQLaddress;

                    cmdCountry.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar));
                    cmdDataset.Parameters.Add(new SqlParameter("@country_id", SqlDbType.Int));
                    cmdDataset.Parameters.Add(new SqlParameter("@year", SqlDbType.Int));
                    cmdDataset.Parameters.Add(new SqlParameter("@description", SqlDbType.NVarChar));
                    cmdDataset.Parameters.Add(new SqlParameter("@date_imported", SqlDbType.DateTime));
                    cmdCity.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar));
                    cmdCity.Parameters.Add(new SqlParameter("@country_id", SqlDbType.Int));
                    cmdAddress.Parameters.Add(new SqlParameter("@city_id", SqlDbType.Int));
                    cmdAddress.Parameters.Add(new SqlParameter("@street", SqlDbType.NVarChar));
                    cmdAddress.Parameters.Add(new SqlParameter("@dataset_id", SqlDbType.Int));

                    int countryId, cityId, datasetId;
                    string description;

                    try
                    {

                        cmdCountry.Parameters["@name"].Value = data.Name;
                        countryId = (int)cmdCountry.ExecuteScalar();

                        description = $"dataset for country {data.Name}, {DateTime.Now.Year}";
                        cmdDataset.Parameters["@description"].Value = description;
                        cmdDataset.Parameters["@year"].Value = DateTime.Now.Year;
                        cmdDataset.Parameters["@date_imported"].Value = DateTime.Now;
                        datasetId = (int)cmdDataset.ExecuteScalar();

                        foreach (CityDTO city in data.Cities)
                        {
                            cmdCity.Parameters["@name"].Value = city.Name;
                            cmdCity.Parameters["@country_id"].Value = countryId;
                            cityId = (int)cmdCity.ExecuteScalar();
                            foreach (string street in city.Addresses)
                            {
                                cmdAddress.Parameters["@city_id"].Value = cityId;
                                cmdAddress.Parameters["@street"].Value = street;
                                cmdAddress.Parameters["@dataset_id"].Value = datasetId;
                                cmdAddress.ExecuteNonQuery();
                            }
                        }
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

        
        public void InsertFirstName(FirstNameDTO entry)
        {
            string SQLfirstName = "INSERT INTO first_name(name, frequency, gender, dataset_id) VALUES(@name, @gender, @frequency, @dataset_id)";
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
                try
                {
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