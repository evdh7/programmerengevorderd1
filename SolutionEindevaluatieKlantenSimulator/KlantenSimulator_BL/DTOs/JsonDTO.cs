using System.Text.Json.Serialization;

namespace KlantenSimulatorBL.DTOs
{
    public class JsonDTO
    {
        public class FileJsonDTO
        {
            public string Title { get; set; }
            public AddressSection Address { get; set; } = new();
            public NameSection Name { get; set; } = new ();

        }

        public class AddressSection
        {
            [JsonPropertyName("city_name")]
            public List<string> City_Names { get; set; } = new();

            [JsonPropertyName("street")]
            public List<string> Streets { get; set; } = new();
        }
        public class NameSection
        {
            [JsonPropertyName("male_first_name")]
            public List<string> Male_First_Names { get; set; } = new();

            [JsonPropertyName("female_first_name")]
            public List<string> Female_First_Names { get; set; } = new();

            [JsonPropertyName("male_last_name")]
            public List<string> Male_Last_Names { get; set; } = new();

            [JsonPropertyName("female_last_name")]
            public List<string> Female_Last_Names { get; set; } = new();

            [JsonPropertyName("last_name")]
            public List<string> Last_Names { get; set; } = new();
        }
    }




}
