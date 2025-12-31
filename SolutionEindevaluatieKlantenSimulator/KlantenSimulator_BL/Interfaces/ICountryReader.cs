using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Enums;

namespace KlantenSimulatorBL.Interfaces
{

    public interface ICountryReader : IAddressReader, INameReader
    {
        new List<NameDTO.NameEntry> ReadNames(string folder, (string, string)[] fileNames, NameType type, Gender? gender);
    }
}
