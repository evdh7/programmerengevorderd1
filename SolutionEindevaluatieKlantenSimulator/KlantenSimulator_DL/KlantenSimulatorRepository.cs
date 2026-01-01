using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Exceptions;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorBL.Manager;
using KlantenSimulatorBL.Model;
using Microsoft.Data.SqlClient;
using System.Data;


namespace KlantenSimulatorDL_SQL
{
    public class KlantenSimulatorRepository(string connectionString) : IFileRepository
    {
        private readonly string connectionString = connectionString;

        public int InsertAddress(CountryDTO data)
        {
            using SqlConnection conn = new(connectionString);
            conn.Open();
            using SqlTransaction sqlTransaction = conn.BeginTransaction();
            int datasetId = 0;

            var knownStreets = new Dictionary<(int, string), bool>();

            try
            {
                PrepareCommandsAddresses(conn, sqlTransaction, out var cmdCountry, out var cmdCity, out var streetTable, out var cmdDataset, out var addressTable);

                int countryId = GetOrInsertCountry(cmdCountry, data.Name);
                datasetId = InsertDataset(cmdDataset, data.Name, countryId);

                if (data.Addresses?.Any() == true) //some country info has a list of streets with no linked cities
                {
                    int cityId = GetOrInsertCity(cmdCity, countryId, "Unknown City");

                    foreach (string street in data.Addresses)
                    {
                        GetStreet(knownStreets, streetTable, cityId, street);
                    }
                }

                foreach (CityDTO city in data.Cities)
                {
                    int cityId = GetOrInsertCity(cmdCity, countryId, city.Name);

                    if (city.Addresses?.Any() == true)
                    {
                        foreach (string street in city.Addresses)
                        {
                            GetStreet(knownStreets, streetTable, cityId, street);
                        }
                    }
                }

                using (var bulk = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, sqlTransaction))
                {
                    bulk.DestinationTableName = "street";
                    bulk.ColumnMappings.Add("city_id", "city_id");
                    bulk.ColumnMappings.Add("street_raw", "street_raw");
                    bulk.ColumnMappings.Add("street_normalized", "street_normalized");
                    bulk.WriteToServer(streetTable);
                }

                var streetLookup = StreetLookup(conn, sqlTransaction);

                foreach (var kvp in knownStreets)
                {
                    var (cityId, normalized) = kvp.Key;
                    int streetId = streetLookup[(cityId, normalized)];

                    addressTable.Rows.Add(streetId, datasetId);
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
            catch (KlantenSimulatorException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                sqlTransaction.Rollback();
            }

            return datasetId;
        }
        private static Dictionary<(int, string), int> StreetLookup(SqlConnection conn, SqlTransaction sqlTransaction)
        {
            Dictionary<(int, string), int> lookup = [];

            SqlCommand cmd = new("SELECT id, city_id, street_normalized FROM street", conn, sqlTransaction);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    int cityId = reader.GetInt32(1);
                    string normalized = reader.GetString(2);
                    lookup[(cityId, normalized)] = id;
                }
            }
            return lookup;
        }

        private static string NormalizeInput(string input)
        {
            return input.Trim().ToLowerInvariant().Replace("  ", " ").Normalize();
        }

        private static void PrepareCommandsAddresses(SqlConnection conn, SqlTransaction sqlTransaction, out SqlCommand cmdCountry, out SqlCommand cmdCity, out DataTable streetTable, out SqlCommand cmdDataset, out DataTable addressTable)
        {
            string SQLcountry = "SELECT id FROM country WHERE name = @name";
            string SQLdataset = "INSERT INTO dataset(country_id, year, description, date_imported) OUTPUT INSERTED.ID VALUES(@country_id,@year,@description,@date_imported)";
            string SQLcity = "SELECT id FROM city WHERE country_id = @country_id AND name = @name";
            //string SQLstreet = "INSERT INTO street(city_id, street_raw, street_normalized) OUTPUT INSERTED.ID VALUES(@city_id, @street_raw, @street_normalized)";
            //string SQLselectStreetId = "SELECT id FROM street WHERE city_id = @city_id AND street_normalized = @street_normalized";

            streetTable = new DataTable();
            streetTable.Columns.Add("city_id", typeof(int));
            streetTable.Columns.Add("street_raw", typeof(string));
            streetTable.Columns.Add("street_normalized", typeof(string));


            addressTable = new DataTable();
            addressTable.Columns.Add("street_id", typeof(int));
            addressTable.Columns.Add("dataset_id", typeof(int));

            cmdCountry = conn.CreateCommand();
            cmdCity = conn.CreateCommand();
            //cmdStreet = conn.CreateCommand();
            cmdDataset = conn.CreateCommand();
            //cmdSelectStreetId = conn.CreateCommand();

            cmdCountry.Transaction = sqlTransaction;
            cmdDataset.Transaction = sqlTransaction;
            cmdCity.Transaction = sqlTransaction;
            //cmdStreet.Transaction = sqlTransaction;
            //cmdSelectStreetId.Transaction = sqlTransaction;

            cmdCountry.CommandText = SQLcountry;
            cmdDataset.CommandText = SQLdataset;
            cmdCity.CommandText = SQLcity;
            //cmdStreet.CommandText = SQLstreet;
            // cmdSelectStreetId.CommandText = SQLselectStreetId;


            cmdCountry.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar));
            cmdDataset.Parameters.Add(new SqlParameter("@country_id", SqlDbType.Int));
            cmdDataset.Parameters.Add(new SqlParameter("@year", SqlDbType.Int));
            cmdDataset.Parameters.Add(new SqlParameter("@description", SqlDbType.NVarChar));
            cmdDataset.Parameters.Add(new SqlParameter("@date_imported", SqlDbType.DateTime));
            cmdCity.Parameters.Add(new SqlParameter("@country_id", SqlDbType.Int));
            cmdCity.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar));
            // cmdStreet.Parameters.Add(new SqlParameter("@city_id", SqlDbType.Int));
            // cmdStreet.Parameters.Add(new SqlParameter("@street_raw", SqlDbType.NVarChar));
            // cmdStreet.Parameters.Add(new SqlParameter("@street_normalized", SqlDbType.NVarChar));
            // cmdSelectStreetId.Parameters.Add(new SqlParameter("@city_id", SqlDbType.Int));
            // cmdSelectStreetId.Parameters.Add(new SqlParameter("@street_normalized", SqlDbType.NVarChar));

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

        private static void GetStreet(Dictionary<(int cityId, string normalized), bool> knownStreets, DataTable streetTable, int cityId, string street)
        {
            string normalized = NormalizeInput(street);

            if (!knownStreets.ContainsKey((cityId, normalized)))
            {
                knownStreets[(cityId, normalized)] = true;
                streetTable.Rows.Add(cityId, street, normalized);
            }
        }

        public void InsertName(List<NameDTO.NameEntry> data, int datasetId)
        {
            string SQLgenderLookup = "SELECT id FROM gender WHERE gender = @gender";

            using SqlConnection conn = new(connectionString);
            using SqlCommand cmdGender = conn.CreateCommand();
            conn.Open();
            using SqlTransaction sqlTransaction = conn.BeginTransaction();
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
                            string? genderKey = entry.Gender.ToString(); //de ids in gendertable zijn vast, die steken we in een string

                            if (!genderIds.TryGetValue(genderKey, out int id)) //if key is not in dictionary
                            {
                                cmdGender.Parameters["@gender"].Value = genderKey;
                                var result = cmdGender.ExecuteScalar() ?? throw new Exception($"Gender lookup failed for '{genderKey}'");
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


                    catch (KlantenSimulatorException ex)
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
                    catch (KlantenSimulatorException ex)
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
                    catch (KlantenSimulatorException ex)
                    {
                        Console.WriteLine("Bulk insert failed: " + ex.Message); foreach (DataRow row in firstNameTable.Rows) { if (row.IsNull("gender_id")) Console.WriteLine("Null gender row: " + row["debug_name"]); }
                        throw;
                    }
                }

                sqlTransaction.Commit();
            }

            catch (KlantenSimulatorException ex)
            {
                Console.WriteLine($"Error: " + ex.Message);

                sqlTransaction.Rollback();
            }
        }

        public List<string> GetCountries()
        {
            string query = "SELECT name FROM country";
            List<string> countries = [];
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = connection.CreateCommand();
            try
            {
                command.CommandText = query;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    countries.Add(reader.GetString(0));

                }
            }
            catch (KlantenSimulatorException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return countries;
        }
        public List<City> GetCities(string countryName)
        {
            List<City> cities = [];
            string query = $"SELECT city.name FROM city LEFT JOIN country on country_id = country.id WHERE country.name = @countryName";
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = connection.CreateCommand();
            try
            {
                command.CommandText = query;
                command.Parameters.AddWithValue("@countryName", countryName);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    cities.Add(new City(reader.GetString(0)));
                }
            }
            catch (KlantenSimulatorException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return cities;

        }

        public IEnumerable<Dataset> GetDataSet(string countryName)
        {
            List<Dataset> datasets = [];
            string query = $"SELECT dataset.description, dataset.date_imported FROM dataset LEFT JOIN country on country_id = country.id WHERE country.name = @countryName";
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = connection.CreateCommand();
            try
            {
                command.CommandText = query;
                command.Parameters.AddWithValue("@countryName", countryName);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string description = reader.GetString(0);
                    DateTime dateImported = reader.GetDateTime(1);
                    datasets.Add(new Dataset(description, dateImported));
                }

            }
            catch (KlantenSimulatorException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return datasets;
        }

        public List<Address> StartSimulation(SimulationParameters parameters)
        {
            var (streetNames, cities) = GetStreetDataForSimulation(parameters);

            var simulator = new AddressSimulator(
                streetNames,
                cities,
                parameters.MaxHousenumber,
                parameters.PercentageLetters
            );

            return simulator.GetAddresses(parameters.AmountOfCustomers);
        }

        public (List<string> streetNames, List<(int cityId, string cityName)> cities) GetStreetDataForSimulation(SimulationParameters parameters)
        {
            List<string> streetNames = new();
            List<(int cityId, string cityName)> cities = new();

            using SqlConnection connection = new(connectionString);
            using SqlCommand command = connection.CreateCommand();

            string cityParams = string.Join(",", parameters.SelectedCities.Select((c, i) => $"@city{i}"));

            command.CommandText = $@"SELECT street.street_raw, street.city_id, city.name FROM street INNER JOIN city ON street.city_id = city.id INNER JOIN country ON city.country_id = country.id WHERE country.name = @countryName AND city.name IN ({cityParams})";
            command.Parameters.AddWithValue("@countryName", parameters.Country);

            for (int i = 0; i < parameters.SelectedCities.Count; i++)
            {
                command.Parameters.AddWithValue($"@city{i}", parameters.SelectedCities[i].Name);
            }

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                streetNames.Add(reader.GetString(0));
                cities.Add((reader.GetInt32(1), reader.GetString(2)));

            }

            return (streetNames, cities);
        }

        public int GetCountryId(string countryName)
        {
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT id FROM city INNER JOIN country ON city.country_id = country.id\r\nWHERE country.name = @countryName";
            command.Parameters.AddWithValue("@countryName", countryName);

            connection.Open();
            return (int)command.ExecuteScalar();
        }

    }
}
