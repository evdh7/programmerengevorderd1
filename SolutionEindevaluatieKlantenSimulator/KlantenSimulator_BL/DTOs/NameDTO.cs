using KlantenSimulatorBL.Enums;

namespace KlantenSimulatorBL.DTOs
{

    public class NameDTO
    {
        public NameDTO(string name, Gender gender, int? frequency)
        {
            Name = name;
            Gender = gender;
            Frequency = frequency;
        }

        public NameDTO(string name, Gender gender)
        {
            Name = name;
            Gender = gender;

        }

        public string Name { get; set; }
        public Gender Gender { get; set; }
        public int? Frequency { get; set; } // optional

    }
}

