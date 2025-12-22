using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Enums;
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

        public int InsertAddress(CountryDTO data)
        {
            int datasetId = 0;
            //string SQLcountry = "INSERT INTO country(name) OUTPUT INSERTED.ID VALUES(@name)";
            string SQLcountry = "SELECT id FROM country WHERE name = @name";
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
                    int? countryId, cityId;

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


                    string description;

                    try
                    {
                        cmdCountry.Parameters["@name"].Value = data.Name;

                        var result = cmdCountry.ExecuteScalar();

                        if (result != null)
                        {
                            countryId = (int)result;
                        }
                        else
                        {
                            cmdCountry.CommandText = "INSERT INTO country(name) OUTPUT INSERTED.ID VALUES(@name)";
                            countryId = (int)cmdCountry.ExecuteScalar();
                        }

                        description = $"dataset for country {data.Name}, {DateTime.Now.Year}";
                        cmdDataset.Parameters["@country_id"].Value = countryId;
                        cmdDataset.Parameters["@description"].Value = description;
                        cmdDataset.Parameters["@year"].Value = DateTime.Now.Year;
                        cmdDataset.Parameters["@date_imported"].Value = DateTime.Now;
                        datasetId = (int)cmdDataset.ExecuteScalar();

                        int unknownCityId = 0;

                        if (data.Addresses?.Any() == true)
                        {
                            cmdCity.Parameters["@name"].Value = "Unknown City";
                            cmdCity.Parameters["@country_id"].Value = countryId;
                            unknownCityId = (int)cmdCity.ExecuteScalar();

                            foreach (string street in data.Addresses)
                            {
                                cmdAddress.Parameters["@city_id"].Value = unknownCityId;
                                cmdAddress.Parameters["@street"].Value = street;
                                cmdAddress.Parameters["@dataset_id"].Value = datasetId;
                                cmdAddress.ExecuteNonQuery();
                            }
                        }

                        foreach (CityDTO city in data.Cities)
                        {
                            cmdCity.Parameters["@name"].Value = city.Name;
                            cmdCity.Parameters["@country_id"].Value = countryId;
                            cityId = (int)cmdCity.ExecuteScalar();

                            if (city.Addresses.Count >= 1)
                            {
                                foreach (string street in city.Addresses)
                                {
                                    cmdAddress.Parameters["@city_id"].Value = cityId;
                                    cmdAddress.Parameters["@street"].Value = street;
                                    cmdAddress.Parameters["@dataset_id"].Value = datasetId;
                                    cmdAddress.ExecuteNonQuery();
                                }
                            }
                        }
                        sqlTransaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        sqlTransaction.Rollback();
                    }
                    return datasetId;

                }
            }
        }



        public void InsertName(List<NameDTO.NameEntry> data, int datasetId)
        {
            string SQLgenderLookup = "SELECT id FROM gender WHERE gender = @gender";
            string SQLfirstName = "INSERT INTO first_name(name, frequency, gender_id, dataset_id) VALUES(@name, @frequency, @gender_id, @dataset_id)";
            string SQLlastName = "INSERT INTO last_name(name, frequency, gender_id, dataset_id) VALUES(@name, @frequency, @gender_id, @dataset_id)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmdGender = conn.CreateCommand())
            using (SqlCommand cmdFirstName = conn.CreateCommand())
            using (SqlCommand cmdLastName = conn.CreateCommand())

            {
                conn.Open();
                using (SqlTransaction sqlTransaction = conn.BeginTransaction())
                {
                    cmdGender.Transaction = sqlTransaction;
                    cmdGender.CommandText = SQLgenderLookup;
                    cmdFirstName.Transaction = sqlTransaction;
                    cmdFirstName.CommandText = SQLfirstName;
                    cmdLastName.Transaction = sqlTransaction;
                    cmdLastName.CommandText = SQLlastName;

                    cmdGender.Parameters.Add(new SqlParameter("@gender", SqlDbType.NVarChar));

                    cmdFirstName.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar));
                    cmdFirstName.Parameters.Add(new SqlParameter("@frequency", SqlDbType.Int));
                    cmdFirstName.Parameters.Add(new SqlParameter("@gender_id", SqlDbType.Int));
                    cmdFirstName.Parameters.Add(new SqlParameter("@dataset_id", SqlDbType.Int));

                    cmdLastName.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar));
                    cmdLastName.Parameters.Add(new SqlParameter("@frequency", SqlDbType.Int));
                    cmdLastName.Parameters.Add(new SqlParameter("@gender_id", SqlDbType.Int));
                    cmdLastName.Parameters.Add(new SqlParameter("@dataset_id", SqlDbType.Int));
                    try
                    {

                        foreach (var entry in data)
                        {
                            try
                            {
                                int? genderId = null;
                                var genderIds = new Dictionary<string, int>();


                                if (entry.Gender != null)
                                {
                                    string genderKey = entry.Gender.ToString(); //de ids in gendertable zijn vast, die steken we in een string

                                    if (!genderIds.TryGetValue(genderKey, out int id)) //if key is not in dictionary
                                    {
                                        cmdGender.Parameters["@gender"].Value = genderKey; //value of genderKey is the value of gender_id
                                        genderId = (int)cmdGender.ExecuteScalar();
                                        genderIds[genderKey] = id;
                                    }
                                }

                                if (entry.FirstOrLast == NameType.First)
                                {
                                    cmdFirstName.Parameters["@name"].Value = entry.Name;
                                    cmdFirstName.Parameters["@frequency"].Value = entry.Frequency ?? (object)DBNull.Value;
                                    cmdFirstName.Parameters["@gender_id"].Value = genderId;
                                    cmdFirstName.Parameters["@dataset_id"].Value = datasetId;
                                    cmdFirstName.ExecuteNonQuery();
                                }
                                else if (entry.FirstOrLast == NameType.Last)
                                {
                                    cmdLastName.Parameters["@name"].Value = entry.Name;
                                    cmdLastName.Parameters["@frequency"].Value = entry.Frequency ?? (object)DBNull.Value;
                                    cmdLastName.Parameters["@gender_id"].Value = genderId ?? (object)DBNull.Value;
                                    cmdLastName.Parameters["@dataset_id"].Value = datasetId;
                                    cmdLastName.ExecuteNonQuery();
                                }
                            }

                            catch (Exception ex)
                            {
                                Console.WriteLine(
                                    $"Error inserting name '{entry.Name}' " +
                                    $"(Gender: {entry.Gender}, " +
                                    $"Frequency: {entry.Frequency?.ToString() ?? "NULL"}, " +
                                    $"DatasetId: {datasetId}) " +
                                    $"=> {ex.Message}"
                                );
                            }
                        }
                        sqlTransaction.Commit();
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: " + ex.Message);

                        sqlTransaction.Rollback();
                    }

                }

            }
        }
    }
}