using System.Text.Json.Serialization;

namespace KlantenSimulatorBL.DTOs
{
    public class JsonDTO
    {
        public class FileJsonDTO
        {
            [JsonPropertyName("title")]
            public string Title { get; set; }
            [JsonPropertyName("address")]
            public AddressSection? Address { get; set; } = new();
            [JsonPropertyName("name")]
            public NameSection Name { get; set; } = new();

        }

        public class AddressSection
        {
            [JsonPropertyName("city_name")]
            public List<string> City_Name { get; set; } = [];

            [JsonPropertyName("street")]
            public List<string> Street { get; set; } = [];
        }
        public class NameSection
        {
            [JsonPropertyName("male_first_name")]
            public List<string> Male_First_Name { get; set; } = [];

            [JsonPropertyName("female_first_name")]
            public List<string> Female_First_Name { get; set; } = [];

            [JsonPropertyName("male_last_name")]
            public List<string> Male_Last_Name { get; set; } = [];

            [JsonPropertyName("female_last_name")]
            public List<string> Female_Last_Name { get; set; } = [];

            [JsonPropertyName("first_name_male")]
            public List<string> First_Name_Male { get; set; } = [];

            [JsonPropertyName("first_name_female")]
            public List<string> First_Name_Female { get; set; } = [];

            [JsonPropertyName("last_name")]
            public List<string> Last_Name { get; set; } = [];

        }
    }

}








