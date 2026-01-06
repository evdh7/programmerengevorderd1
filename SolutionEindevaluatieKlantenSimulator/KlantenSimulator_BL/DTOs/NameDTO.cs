using KlantenSimulatorBL.Enums;
using System.Collections;
using static KlantenSimulatorBL.DTOs.NameDTO;

namespace KlantenSimulatorBL.DTOs
{

    public class NameDTO : IEnumerable<NameEntry>
    {
        private readonly List<NameEntry> _names;

        public NameDTO(List<NameDTO.NameEntry> names)
        {
            _names = names;
        }

        public NameDTO()
        {
            _names = new List<NameEntry>();
        }
        public IEnumerator<NameEntry> GetEnumerator()
        {
            return _names.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public class NameEntry
        {
            public string Name { get; set; }
            public NameType FirstOrLast { get; set; }
            public Gender? Gender { get; set; }
            public int? Frequency { get; set; }
            public int CumulativeWeight { get; set; }
            public NameEntry(string name, NameType firstOrLast, Gender? gender, int? frequency)
            {
                Name = name;
                FirstOrLast = firstOrLast;
                Gender = gender;
                Frequency = frequency;
            }

            public NameEntry(string name, NameType firstOrLast, Gender? gender, int? frequency, int cumulativeWeight)
            {
                Name = name;
                FirstOrLast = firstOrLast;
                Gender = gender;
                Frequency = frequency;
                CumulativeWeight = cumulativeWeight;
            }
       }
    }
}
