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
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction sqlTransaction = conn.BeginTransaction())
                {
                    var addressTable = new DataTable();
                    addressTable.Columns.Add("street_id", typeof(int));
                    addressTable.Columns.Add("dataset_id", typeof(int));


                    string description;
                    int datasetId = 0;

                    try
                    {
                        PrepareCommandsAddresses(conn, sqlTransaction, out var cmdCountry, out var cmdCity, out var cmdStreet, out var cmdDataset, out var cmdSelectStreetId);

                        int countryId = GetOrInsertCountry(cmdCountry, data.Name);
                        datasetId = InsertDataset(cmdDataset, data.Name, countryId);

                        if (data.Addresses?.Any() == true) //some country info has a list of streets with no linked cities
                        {
                            int cityId = GetOrInsertCity(cmdCity, countryId, "Unknown City");

                            foreach (string street in data.Addresses)
                            {
                                int streetId = GetOrInsertStreet(cmdSelectStreetId, cmdStreet, cityId, street);

                                addressTable.Rows.Add(streetId, datasetId);
                            }
                        }

                        foreach (CityDTO city in data.Cities)
                        {
                            int cityId = GetOrInsertCity(cmdCity, countryId, city.Name);

                            if (city.Addresses?.Any() == true)
                            {
                                foreach (string street in city.Addresses)
                                {
                                    int streetId = GetOrInsertStreet(cmdSelectStreetId, cmdStreet, cityId, street);

                                    addressTable.Rows.Add(streetId, datasetId);
                                }
                            }
                        }

                        using (var bulk = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, sqlTransaction))
                        {
                            bulk.DestinationTableName = "address";
                            bulk.ColumnMappings.Add("street_id", "street_id");
                            bulk.ColumnMappings.Add("dataset_id", "dataset_id");
                            bulk.WriteToServer(addressTable);
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

        private static string NormalizeInput(string input)
        {
            return input.Trim().ToLowerInvariant().Replace("  ", " ").Normalize();
        }

        private void PrepareCommandsAddresses(SqlConnection conn, SqlTransaction sqlTransaction, out SqlCommand cmdCountry, out SqlCommand cmdCity, out SqlCommand cmdStreet, out SqlCommand cmdDataset, out SqlCommand cmdSelectStreetId)
        {
            string SQLcountry = "SELECT id FROM country WHERE name = @name";
            string SQLdataset = "INSERT INTO dataset(country_id, year, description, date_imported) OUTPUT INSERTED.ID VALUES(@country_id,@year,@description,@date_imported)";
            string SQLcity = "SELECT id FROM city WHERE country_id = @country_id AND name = @name";
            string SQLstreet = "INSERT INTO street(city_id, street_raw, street_normalized) OUTPUT INSERTED.ID VALUES(@city_id, @street_raw, @street_normalized)";
            string SQLselectStreetId = "SELECT id FROM street WHERE city_id = @city_id AND street_normalized = @street_normalized";

            cmdCountry = conn.CreateCommand();
            cmdCity = conn.CreateCommand();
            cmdStreet = conn.CreateCommand();
            cmdDataset = conn.CreateCommand();
            cmdSelectStreetId = conn.CreateCommand();

            cmdCountry.Transaction = sqlTransaction;
            cmdDataset.Transaction = sqlTransaction;
            cmdCity.Transaction = sqlTransaction;
            cmdStreet.Transaction = sqlTransaction;
            cmdSelectStreetId.Transaction = sqlTransaction;

            cmdCountry.CommandText = SQLcountry;
            cmdDataset.CommandText = SQLdataset;
            cmdCity.CommandText = SQLcity;
            cmdStreet.CommandText = SQLstreet;
            cmdSelectStreetId.CommandText = SQLselectStreetId;


            cmdCountry.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar));
            cmdDataset.Parameters.Add(new SqlParameter("@country_id", SqlDbType.Int));
            cmdDataset.Parameters.Add(new SqlParameter("@year", SqlDbType.Int));
            cmdDataset.Parameters.Add(new SqlParameter("@description", SqlDbType.NVarChar));
            cmdDataset.Parameters.Add(new SqlParameter("@date_imported", SqlDbType.DateTime));
            cmdCity.Parameters.Add(new SqlParameter("@country_id", SqlDbType.Int));
            cmdCity.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar));
            cmdStreet.Parameters.Add(new SqlParameter("@city_id", SqlDbType.Int));
            cmdStreet.Parameters.Add(new SqlParameter("@street_raw", SqlDbType.NVarChar));
            cmdStreet.Parameters.Add(new SqlParameter("@street_normalized", SqlDbType.NVarChar));
            cmdSelectStreetId.Parameters.Add(new SqlParameter("@city_id", SqlDbType.Int));
            cmdSelectStreetId.Parameters.Add(new SqlParameter("@street_normalized", SqlDbType.NVarChar));

        }

        private static int GetOrInsertCountry(SqlCommand cmdCountry, string name)
        {
            int countryId;

            cmdCountry.Parameters["@name"].Value = name;

            var result = cmdCountry.ExecuteScalar();

            if (result != null)
            {
                countryId = (int)result;
                return countryId;
            }
            else
            {
                cmdCountry.CommandText = "INSERT INTO country(name) OUTPUT INSERTED.ID VALUES(@name)";
                return (int)cmdCountry.ExecuteScalar();
            }
        }

        private static int InsertDataset(SqlCommand cmdDataset, string country, int countryId)
        {
            string description = $"dataset for country {country}, {DateTime.Now.Year}";
            cmdDataset.Parameters["@country_id"].Value = countryId;
            cmdDataset.Parameters["@description"].Value = description;
            cmdDataset.Parameters["@year"].Value = DateTime.Now.Year;
            cmdDataset.Parameters["@date_imported"].Value = DateTime.Now;
            return (int)cmdDataset.ExecuteScalar();
        }

        private static int GetOrInsertCity(SqlCommand cmdCity, int countryId, string cityName)
        {
            cmdCity.Parameters["@name"].Value = cityName;
            cmdCity.Parameters["@country_id"].Value = countryId;
            var result = cmdCity.ExecuteScalar();

            if (result != null && result != DBNull.Value)
                return (int)result;
            cmdCity.CommandText = "INSERT INTO city(country_id, name) OUTPUT INSERTED.ID VALUES (@country_id, @name)";
            return (int)cmdCity.ExecuteScalar();

        }

        private static int GetOrInsertStreet(SqlCommand cmdSelectStreetId, SqlCommand cmdStreet, int cityId, string street)
        {
            string normalized = NormalizeInput(street);

            cmdSelectStreetId.Parameters["@city_id"].Value = cityId;
            cmdSelectStreetId.Parameters["@street_normalized"].Value = normalized;

            var result = cmdSelectStreetId.ExecuteScalar();

            if (result != null && result != DBNull.Value)
            {
                return (int)result;
            }

            cmdStreet.Parameters["@city_id"].Value = cityId;
            cmdStreet.Parameters["@street_raw"].Value = street;
            cmdStreet.Parameters["@street_normalized"].Value = normalized;
            return (int)cmdStreet.ExecuteScalar();

        }

        public void InsertName(List<NameDTO.NameEntry> data, int datasetId)
        {
            string SQLgenderLookup = "SELECT id FROM gender WHERE gender = @gender";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmdGender = conn.CreateCommand())

            {
                conn.Open();
                using (SqlTransaction sqlTransaction = conn.BeginTransaction())
                {
                    var firstNameTable = new DataTable();
                    firstNameTable.Columns.Add("name", typeof(string));
                    firstNameTable.Columns.Add("frequency", typeof(int));
                    firstNameTable.Columns["frequency"].AllowDBNull = true;
                    firstNameTable.Columns.Add("gender_id", typeof(int));
                    firstNameTable.Columns.Add("dataset_id", typeof(int));

                    var lastNameTable = new DataTable();
                    lastNameTable.Columns.Add("name", typeof(string));
                    lastNameTable.Columns.Add("frequency", typeof(int));
                    lastNameTable.Columns["frequency"].AllowDBNull = true;
                    lastNameTable.Columns.Add("gender_id", typeof(int));
                    lastNameTable.Columns["gender_id"].AllowDBNull = true;
                    lastNameTable.Columns.Add("dataset_id", typeof(int));

                    cmdGender.Transaction = sqlTransaction;
                    cmdGender.CommandText = SQLgenderLookup;


                    cmdGender.Parameters.Add(new SqlParameter("@gender", SqlDbType.NVarChar));
                    try
                    {
                        var genderIds = new Dictionary<string, int>();

                        foreach (var entry in data)
                        {
                            try
                            {
                                int? genderId = null;

                                if (entry.Gender != null)
                                {
                                    string genderKey = entry.Gender.ToString(); //de ids in gendertable zijn vast, die steken we in een string

                                    if (!genderIds.TryGetValue(genderKey, out int id)) //if key is not in dictionary
                                    {
                                        cmdGender.Parameters["@gender"].Value = genderKey;
                                        var result = cmdGender.ExecuteScalar();
                                        if (result == null)
                                            throw new Exception($"Gender lookup failed for '{genderKey}'");
                                        genderId = (int)result;
                                        genderIds[genderKey] = genderId.Value;
                                    }

                                    else
                                    {
                                        genderId = id;
                                    }
                                }
                                if (entry.FirstOrLast == NameType.First)
                                {
                                    firstNameTable.Rows.Add(entry.Name, entry.Frequency ?? (object)DBNull.Value, genderId ?? (object)DBNull.Value, datasetId);
                                }
                                else if (entry.FirstOrLast == NameType.Last)
                                {
                                    lastNameTable.Rows.Add(entry.Name, entry.Frequency ?? (object)DBNull.Value, genderId ?? (object)DBNull.Value, datasetId);
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
                        using (var bulk = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, sqlTransaction))
                        {
                            bulk.DestinationTableName = "first_name";
                            bulk.ColumnMappings.Add("name", "name");
                            bulk.ColumnMappings.Add("frequency", "frequency");
                            bulk.ColumnMappings.Add("gender_id", "gender_id");
                            bulk.ColumnMappings.Add("dataset_id", "dataset_id");
                            try
                            {
                                bulk.WriteToServer(firstNameTable);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Bulk insert failed: " + ex.Message); foreach (DataRow row in firstNameTable.Rows) { if (row.IsNull("gender_id")) Console.WriteLine("Null gender row: " + row["debug_name"]); }
                                throw;
                            }
                        }

                        using (var bulk = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, sqlTransaction))
                        {
                            bulk.DestinationTableName = "last_name";
                            bulk.ColumnMappings.Add("name", "name");
                            bulk.ColumnMappings.Add("frequency", "frequency");
                            bulk.ColumnMappings.Add("gender_id", "gender_id");
                            bulk.ColumnMappings.Add("dataset_id", "dataset_id");

                            try
                            {
                                bulk.WriteToServer(lastNameTable);

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Bulk insert failed: " + ex.Message); foreach (DataRow row in firstNameTable.Rows) { if (row.IsNull("gender_id")) Console.WriteLine("Null gender row: " + row["debug_name"]); }
                                throw;
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