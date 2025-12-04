using KlantenSimulatorBL.Enums;

namespace KlantenSimulatorBL.DTOs
{

    public class FirstNameDTO
    {
        public FirstNameDTO(string name, Gender gender, int? frequency)
        {
            Name = name;
            Gender = gender;
            Frequency = frequency;
        }

        public string Name { get; set; }
        public Gender Gender { get; set; }
        public int? Frequency { get; set; } // optional
    }
}

