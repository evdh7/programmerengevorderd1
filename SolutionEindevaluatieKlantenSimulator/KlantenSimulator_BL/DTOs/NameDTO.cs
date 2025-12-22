using KlantenSimulatorBL.Enums;
using System.Collections;
using static KlantenSimulatorBL.DTOs.NameDTO;

namespace KlantenSimulatorBL.DTOs
{

    public class NameDTO : IEnumerable<NameEntry>
    {
        private readonly List<NameEntry> _names;

        public NameDTO(List<NameEntry> names)
        {
            _names = names;
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
            public NameEntry(string name, NameType firstOrLast, Gender? gender, int? frequency)
            {
                Name = name;
                Gender = gender;
                Frequency = frequency;
                FirstOrLast = firstOrLast;

            }

            public string Name { get; set; }
            public NameType FirstOrLast { get; set; }   
            public Gender? Gender { get; set; }
            public int? Frequency { get; set; }
        }


    }
}

