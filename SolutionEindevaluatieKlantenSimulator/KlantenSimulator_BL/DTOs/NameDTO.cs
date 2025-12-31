using KlantenSimulatorBL.Enums;
using System.Collections;
using static KlantenSimulatorBL.DTOs.NameDTO;

namespace KlantenSimulatorBL.DTOs
{

    public class NameDTO(List<NameDTO.NameEntry> names) : IEnumerable<NameEntry>
    {
        private readonly List<NameEntry> _names = names;

        public IEnumerator<NameEntry> GetEnumerator()
        {
            return _names.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public class NameEntry(string name, NameType firstOrLast, Gender? gender, int? frequency)
        {
            public string Name { get; set; } = name;
            public NameType FirstOrLast { get; set; } = firstOrLast;
            public Gender? Gender { get; set; } = gender;
            public int? Frequency { get; set; } = frequency;
        }


    }
}


