using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Enums;
using System.Runtime.CompilerServices;

namespace KlantenSimulatorBL.Interfaces
{
    public interface INameDTOIterable
    {
        IEnumerable<NameDTO.NameEntry> GetAll();
    }

    public class GenderedLastNames : INameDTOIterable
    {
        public NameDTO femaleLastName;
        public NameDTO maleLastName;

        public IEnumerable<NameDTO.NameEntry> GetAll()
        {
            return femaleLastName.Concat(maleLastName);
        }
    }

    public class UngenderedLastNames : INameDTOIterable
    {
        public NameDTO lastNames;
        public IEnumerable<NameDTO.NameEntry> GetAll()
        {
            return lastNames;
        }
    }

    public class Names
    {
        public NameDTO maleFirstNames;
        public NameDTO femaleFirstNames;
        public INameDTOIterable lastNames;
    }

    public interface INameReader
    {
       List<NameDTO.NameEntry> ReadNames(string folder, (string,string)[] files, NameType type, Gender? gender);

    }


}
