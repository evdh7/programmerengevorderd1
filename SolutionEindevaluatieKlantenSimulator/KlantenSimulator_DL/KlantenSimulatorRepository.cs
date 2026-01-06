using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Exceptions;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorBL.Manager;
using KlantenSimulatorBL.Model;
using Microsoft.Data.SqlClient;
using System.Data;
using static KlantenSimulatorBL.DTOs.NameDTO;


namespace KlantenSimulatorDL_SQL
{
    public class KlantenSimulatorRepository(string connectionString) : IFileRepository
    {
        private readonly string connectionString = connectionString;

        //INSERT ADDRESS INTO DATABASE//

        public int InsertAddress(CountryDTO data)
        {
            using SqlConnection conn = new(connectionString);
            conn.Open();
            using SqlTransaction sqlTransaction = conn.BeginTransaction();
            int datasetId = 0;

            var knownStreets = new Dictionary<(int?, string), bool>();

            try
            {
                PrepareCommandsAddresses(conn, sqlTransaction, out var cmdCountry, out var cmdCity, out var streetTable, out var cmdDataset, out var addressTable);

                int countryId = GetOrInsertCountry(cmdCountry, data.Name);
                datasetId = InsertDataset(cmdDataset, data.Name, countryId);

                if (data.Addresses?.Count > 0) //some country info has a list of streets with no linked cities
                {
                    foreach (string street in data.Addresses)
                    {
                        GetStreet(knownStreets, streetTable, null, street);
                    }
                }

                foreach (CityDTO city in data.Cities)
                {
                    int cityId = GetOrInsertCity(cmdCity, countryId, city.Name);

                    if (city.Addresses?.Count > 0)
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

        private static void PrepareCommandsAddresses(SqlConnection conn, SqlTransaction sqlTransaction, out SqlCommand cmdCountry, out SqlCommand cmdCity, out DataTable streetTable, out SqlCommand cmdDataset, out DataTable addressTable)
        {
            string SQLcountry = "SELECT id FROM country WHERE name = @name";
            string SQLdataset = "INSERT INTO dataset(country_id, year, description, date_imported) OUTPUT INSERTED.ID VALUES(@country_id,@year,@description,@date_imported)";
            string SQLcity = "SELECT id FROM city WHERE country_id = @country_id AND name = @name";

            streetTable = new DataTable();
            streetTable.Columns.Add("city_id", typeof(int));
            streetTable.Columns["city_id"].AllowDBNull = true;
            streetTable.Columns.Add("street_raw", typeof(string));
            streetTable.Columns.Add("street_normalized", typeof(string));


            addressTable = new DataTable();
            addressTable.Columns.Add("street_id", typeof(int));
            addressTable.Columns.Add("dataset_id", typeof(int));

            cmdCountry = conn.CreateCommand();
            cmdCity = conn.CreateCommand();
            cmdDataset = conn.CreateCommand();

            cmdCountry.Transaction = sqlTransaction;
            cmdDataset.Transaction = sqlTransaction;
            cmdCity.Transaction = sqlTransaction;

            cmdCountry.CommandText = SQLcountry;
            cmdDataset.CommandText = SQLdataset;
            cmdCity.CommandText = SQLcity;


            cmdCountry.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar));
            cmdDataset.Parameters.Add(new SqlParameter("@country_id", SqlDbType.Int));
            cmdDataset.Parameters.Add(new SqlParameter("@year", SqlDbType.Int));
            cmdDataset.Parameters.Add(new SqlParameter("@description", SqlDbType.NVarChar));
            cmdDataset.Parameters.Add(new SqlParameter("@date_imported", SqlDbType.DateTime));
            cmdCity.Parameters.Add(new SqlParameter("@country_id", SqlDbType.Int));
            cmdCity.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar));

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
        private static void GetStreet(Dictionary<(int? cityId, string normalized), bool> knownStreets, DataTable streetTable, int? cityId, string street)
        {
            string normalized = NormalizeStreetname(street);

            if (!knownStreets.ContainsKey((cityId, normalized)))
            {
                knownStreets[(cityId, normalized)] = true;
                streetTable.Rows.Add(cityId == 0 ? DBNull.Value : cityId, street, normalized);
            }
        }
        private static string NormalizeStreetname(string input)
        {
            return input.Trim().ToLowerInvariant().Replace("  ", " ").Normalize();
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
        private static Dictionary<(int?, string), int> StreetLookup(SqlConnection conn, SqlTransaction sqlTransaction)
        {
            Dictionary<(int? cityId, string), int> lookup = [];

            SqlCommand cmd = new("SELECT id, city_id, street_normalized FROM street", conn, sqlTransaction);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    int? cityId = reader.IsDBNull(1) ? null : reader.GetInt32(1);
                    string normalized = reader.GetString(2);
                    lookup[(cityId, normalized)] = id;
                }
            }
            return lookup;
        }


        //INSERT NAMES INTO DATABASE//

        public void InsertName(List<NameEntry> data, int datasetId)
        {
            string SQLgenderLookup = "SELECT id FROM gender WHERE gender = @gender";

            using SqlConnection conn = new(connectionString);
            conn.Open();
            using SqlTransaction sqlTransaction = conn.BeginTransaction();

            try
            {
                PrepareCommandsNames(conn, sqlTransaction, SQLgenderLookup, out var cmdGender, out var firstNameTable, out var lastNameTable);

                var genderIds = new Dictionary<string, int>();

                data.Sort((a, b) => (a.Frequency ?? 0).CompareTo(b.Frequency ?? 0));


                var merged = new Dictionary<(string Name, NameType Type, Gender? Gender), NameEntry>();

                foreach (var entry in data)
                {
                    try
                    {
                        string name = NormalizeName(entry.Name);
                        int frequency = entry.Frequency ?? 1;

                        var key = (Name: name, Type: entry.FirstOrLast, Gender: entry.Gender);

                        if (merged.TryGetValue(key, out var existing)) //if the normalized name with this Gender is already in the dictionary we merge the two frequencies. This way we avoid two entries for Pela and pela, Marie-lou and Marie-Lou etc.
                        {
                            existing.Frequency += frequency;
                            continue;
                        }
                        else
                        {
                            merged[key] = new NameEntry(name, entry.FirstOrLast, entry.Gender, frequency);
                        }

                    }
                    catch (KlantenSimulatorException ex)
                    {
                        Console.WriteLine(
                            $"Error inserting name '{entry.Name}' " +
                            $"(Gender: {entry.Gender}, " +
                            $"Frequency: {entry.Frequency?.ToString() ?? "NULL"}, " +
                            $"DatasetId: {datasetId}) " +
                            $"=> {ex.Message}");
                    }
                }

                AddNamesToTables(firstNameTable, lastNameTable, merged, cmdGender, genderIds, datasetId);


                using (var bulk = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, sqlTransaction))
                {
                    bulk.DestinationTableName = "first_name";
                    bulk.ColumnMappings.Add("name", "name");
                    bulk.ColumnMappings.Add("frequency", "frequency");
                    bulk.ColumnMappings.Add("gender_id", "gender_id");
                    bulk.ColumnMappings.Add("dataset_id", "dataset_id");
                    bulk.ColumnMappings.Add("cumulative_weight", "cumulative_weight");

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
                    bulk.ColumnMappings.Add("cumulative_weight", "cumulative_weight");

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

        private static void PrepareCommandsNames(SqlConnection conn, SqlTransaction sqlTransaction, string SQLgenderLookup, out SqlCommand cmdGender, out DataTable firstNameTable, out DataTable lastNameTable)
        {
            cmdGender = conn.CreateCommand();

            firstNameTable = new DataTable();
            firstNameTable.Columns.Add("name", typeof(string));
            firstNameTable.Columns.Add("frequency", typeof(int));
            firstNameTable.Columns["frequency"].AllowDBNull = true;
            firstNameTable.Columns.Add("gender_id", typeof(int));
            firstNameTable.Columns.Add("dataset_id", typeof(int));
            firstNameTable.Columns.Add("cumulative_weight", typeof(int));

            lastNameTable = new DataTable();
            lastNameTable.Columns.Add("name", typeof(string));
            lastNameTable.Columns.Add("frequency", typeof(int));
            lastNameTable.Columns["frequency"].AllowDBNull = true;
            lastNameTable.Columns.Add("gender_id", typeof(int));
            lastNameTable.Columns["gender_id"].AllowDBNull = true;
            lastNameTable.Columns.Add("dataset_id", typeof(int));
            lastNameTable.Columns.Add("cumulative_weight", typeof(int));

            cmdGender.Transaction = sqlTransaction;
            cmdGender.CommandText = SQLgenderLookup;

            cmdGender.Parameters.Add(new SqlParameter("@gender", SqlDbType.NVarChar));
        }
        //private static string NormalizeFirstName(string name)
        //{
        //    name = name.Trim();
        //    name = name

        //        .Replace("’", "'")
        //        .Replace("`", "'")
        //        .Replace("´", "'");

        //    string? foundSeparator = null;
        //    bool separatorSeen = false;

        //    for (int i = 0; i < name.Length; i++)
        //    {
        //        char c = name[i];

        //        if (c == '-' || c == ' ')
        //        {
        //            foundSeparator = c.ToString();
        //            separatorSeen = true;
        //            break;
        //        }          
                
        //    }

        //    if (foundSeparator == null)
        //    {
        //        return name = char.ToUpper(name[0]) + name[1..].ToLowerInvariant();
        //    }

        //    if (foundSeparator != null)
        //    {
        //        var parts = name.Split(foundSeparator, StringSplitOptions.RemoveEmptyEntries);
                
        //        for (int i = 0; i < parts.Length; i++)
        //        {
        //            var p = parts[i].ToLowerInvariant();
        //            parts[i] = char.ToUpper(p[0]) + p[1..];
        //        }
        //        return string.Join(foundSeparator, parts);
        //    }
        //    return name;
        //}
        private static string NormalizeName(string name)
        {
            if (IsAlreadyNormalized(name)) 
                return name;

            HashSet<string> particles = new(StringComparer.OrdinalIgnoreCase) { "van", "der", "de", "den", "la", "le", "du" };
            
            name = name.Trim();
            var parts = name.Split(new[] { ' ', '-' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Count()<=1)
            {
                return char.ToUpper(name[0]) + name[1..].ToLowerInvariant();
            }

            else
            {
                string? foundSeparator = null;

                foreach (char c in name)
                {
                    if (c == ' ' || c == '-')
                    {
                        foundSeparator = c.ToString();
                        break;
                    }
                }

                for (int i = 0; i < parts.Length; i++)
                {
                    if ((parts[i].Length > 0 && !particles.Contains(parts[i])))
                    {
                        var p = parts[i].ToLowerInvariant();
                        parts[i] = char.ToUpper(p[0]) + p[1..];

                    }

                }
                return string.Join(foundSeparator, parts);
            }
                
        }
        private static bool IsAlreadyNormalized(string name)
        {
            var parts = name.Split(new[] { ' ', '-' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var part in parts)
            {
                if (part.Length == 0)
                    continue;

                // First letter must be uppercase
                if (!char.IsUpper(part[0]))
                    return false;

                // Rest must be lowercase
                for (int i = 1; i < part.Length; i++)
                {
                    if (!char.IsLower(part[i]))
                        return false;
                }
            }

            return true;
        }


        private void AddNamesToTables(DataTable firstNameTable, DataTable lastNameTable, Dictionary<(string Name, NameType Type, Gender? Gender), NameEntry> merged, SqlCommand cmdGender, Dictionary<string, int> genderIds, int datasetId)
        {
            int cumulativeFirst = 0;
            int cumulativeLast = 0;

            foreach (var m in merged.Values)
            {
                int? genderId = null;

                if (m.Gender != null)
                {
                    string? genderKey = m.Gender.ToString(); //de ids in gendertable zijn vast, die steken we in een string

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
                firstNameTable.MinimumCapacity = merged.Count;
                lastNameTable.MinimumCapacity = merged.Count;

                if (m.FirstOrLast == NameType.First)
                {
                    int freq = m.Frequency ?? 1;
                    cumulativeFirst += freq;
                    firstNameTable.Rows.Add(m.Name, freq, genderId ?? (object)DBNull.Value, datasetId, cumulativeFirst);
                }
                else if (m.FirstOrLast == NameType.Last)
                {
                    int freq = m.Frequency ?? 1;
                    cumulativeLast += freq;
                    lastNameTable.Rows.Add(m.Name, freq, genderId ?? (object)DBNull.Value, datasetId, cumulativeFirst);
                }
            }
        }

        //GET SIMULATION DATA //
        public Dictionary<int, string> GetCountries()
        {
            string query = "SELECT id, name FROM country";
            Dictionary<int, string> countries = [];
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = connection.CreateCommand();
            try
            {
                command.CommandText = query;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    countries.Add(reader.GetInt32(0), reader.GetString(1));

                }
            }
            catch (KlantenSimulatorException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return countries;
        }
        public List<CityDTO> GetCities(string countryName)
        {
            List<CityDTO> cities = [];
            string query = $"SELECT city.id, city.name FROM city LEFT JOIN country on country_id = country.id WHERE country.name = @countryName";
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
                    cities.Add(new CityDTO(reader.GetInt32(0), reader.GetString(1)));
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
            string query = $"SELECT dataset.id, dataset.description, dataset.date_imported FROM dataset LEFT JOIN country on country_id = country.id WHERE country.name = @countryName";
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
                    int id = reader.GetInt32(0);
                    string description = reader.GetString(1);
                    DateTime dateImported = reader.GetDateTime(2);
                    datasets.Add(new Dataset(id, description, dateImported));
                }

            }
            catch (KlantenSimulatorException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return datasets;
        }
        private List<NameEntry> GetFirstNameEntries(SimulationParameters parameters)
        {
            List<NameEntry> firstNames = new List<NameEntry>();
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = connection.CreateCommand();

            int datasetId = parameters.SelectedDataset.DatasetId;

            command.CommandText = $@"SELECT name, frequency, gender, cumulative_weight
                                     FROM first_name 
                                     JOIN gender on first_name.gender_id = gender.id
                                     WHERE dataset_id = @datasetId";

            command.Parameters.AddWithValue("datasetId", @datasetId);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                firstNames.Add(new NameEntry(reader.GetString(0), NameType.First, (Gender)Enum.Parse(typeof(Gender), reader.GetString(2)), reader.GetInt32(1), reader.GetInt32(3)));
            }

            return firstNames;
        }

        private List<NameEntry> GetLastNameEntries(SimulationParameters parameters)
        {
            List<NameEntry> lastNames = new List<NameEntry>();
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = connection.CreateCommand();

            int datasetId = parameters.SelectedDataset.DatasetId;

            command.CommandText = $@"SELECT name, frequency, gender, cumulative_weight
                                     FROM last_name 
                                     LEFT JOIN gender on last_name.gender_id = gender.id
                                     WHERE dataset_id = @datasetId";

            command.Parameters.AddWithValue("datasetId", @datasetId);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string genderValue = reader.IsDBNull(2) ? null : reader.GetString(2);
                Gender gender = Gender.Unknown;
                if (!string.IsNullOrEmpty(genderValue))
                {
                    gender = (Gender)Enum.Parse(typeof(Gender), genderValue);
                }
                lastNames.Add(new NameEntry(reader.GetString(0), NameType.Last, gender, reader.GetInt32(1), reader.GetInt32(3)));
            }
            return lastNames;
        }
        public CountryDTO GetCitiesWithStreets(SimulationParameters parameters)
        {
            using SqlConnection connection = new(connectionString);
            using SqlCommand command = connection.CreateCommand();

            int datasetId = parameters.SelectedDataset.DatasetId;

            List<CityDTO> cities = [];

            HashSet<string> streets = [];

            string cityParams = string.Join(",", parameters.SelectedCities.Select((c, i) => $"@city{i}"));

            if (parameters.HasLinkedStreetsAndCities == false)
            {
                CountryDTO country = GetSelectedCitiesAndAllStreets(connection, command, datasetId, parameters, cityParams, cities, streets);
                return country;
            }
            else
            {
                CountryDTO country = GetSelectedCitiesAndStreets(connection, command, datasetId, parameters, cityParams, cities, streets);
                return country;
            }
        }
        private CountryDTO GetSelectedCitiesAndAllStreets(SqlConnection connection, SqlCommand command, int datasetId, SimulationParameters parameters, string cityParams, List<CityDTO> cities, HashSet<string> streets)
        {
            CountryDTO country = new CountryDTO(parameters.CountryName);

            command.CommandText = $@"SELECT id, name FROM city WHERE city.id IN ({cityParams})";

            command.Parameters.AddWithValue("@countryId", parameters.CountryId);
            for (int i = 0; i < parameters.SelectedCities.Count; i++)
            {
                command.Parameters.AddWithValue($"@city{i}", parameters.SelectedCities[i].Id);
            }
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            var cityDictionary = new Dictionary<int, CityDTO>();

            while (reader.Read())
            {
                cities.Add(new CityDTO(reader.GetString(1)) { Id = reader.GetInt32(0) });
            }
            reader.Close();
            command.Parameters.Clear();

            command.CommandText = $@"SELECT street_raw FROM street JOIN address ON street.id = address.street_id WHERE dataset_id = @datasetId";

            command.Parameters.AddWithValue("@datasetId", datasetId);

            reader = command.ExecuteReader();

            while (reader.Read())
            {
                streets.Add(reader.GetString(0));
            }

            country.Addresses = streets;
            country.Cities = cities;
            return country;
        }

        private static CountryDTO GetSelectedCitiesAndStreets(SqlConnection connection, SqlCommand command, int datasetId, SimulationParameters parameters, string cityParams, List<CityDTO> cities, HashSet<string> streets)
        {
            CountryDTO country = new CountryDTO(parameters.CountryName);

            command.CommandText = $@"SELECT city.id, city.name, street.street_raw FROM street JOIN address ON address.street_id = street.id JOIN city ON street.city_id = city.id WHERE city.id IN ({cityParams}) AND address.dataset_id = @datasetId";

            command.Parameters.AddWithValue("@datasetId", datasetId);

            for (int i = 0; i < parameters.SelectedCities.Count; i++)
            {
                command.Parameters.AddWithValue($"@city{i}", parameters.SelectedCities[i].Id);
            }
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            var cityDictionary = new Dictionary<int, CityDTO>();

            while (reader.Read())
            {
                int cityId = reader.GetInt32(0);
                string cityName = reader.GetString(1);
                string street = reader.GetString(2);

                if (!cityDictionary.TryGetValue(cityId, out CityDTO city))
                {
                    city = new CityDTO(cityName)
                    {
                        Id = cityId
                    };
                    cityDictionary.Add(cityId, city);
                }
                city.Addresses.Add(street);
            }
            cities = cityDictionary.Values.ToList();
            country.Cities = cities;
            return country;

        }
        public List<Person> StartSimulation(SimulationParameters parameters)
        {
            List<NameEntry> firstNames = GetFirstNameEntries(parameters);
            List<NameEntry> lastNames = GetLastNameEntries(parameters);
            CountryDTO country = GetCitiesWithStreets(parameters);

            var addressSim = new AddressSimulator(country, parameters.HasLinkedStreetsAndCities, parameters.MaxHousenumber, parameters.PercentageLetters);

            var addresses = addressSim.GetAddresses(parameters.AmountOfCustomers);

            var personSim = new PersonSimulator(firstNames, lastNames, addresses, parameters.MinAge, parameters.MaxAge);

            return personSim.MakePerson(parameters.AmountOfCustomers);
        }

    }
}
