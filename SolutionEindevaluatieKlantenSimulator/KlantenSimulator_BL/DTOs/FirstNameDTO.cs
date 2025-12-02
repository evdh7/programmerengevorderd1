namespace KlantenSimulatorBL.DTOs
{

    public class FirstNameDTO
    {
        public FirstNameDTO(string name, string gender, int? frequency, string country)
        {
            Name = name;
            Gender = gender;
            Frequency = frequency;
            Country = country;
        }

        public string Name { get; set; }
        public string Gender { get; set; }   // "M" or "F"
        public int? Frequency { get; set; } // optional
        public string Country { get; set; } // 
    }
}

