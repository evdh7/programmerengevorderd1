using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Interfaces;

namespace KlantenSimulatorUtils
{
    public static class KlantenSimulatorFileReaderFactory
    {
        //public static IAddressReader GetAddressReader(string folder, string fileName, string country)
        //{
        //    string extension = Path.GetExtension(fileName).ToLower(); //unit

        //    if (extension == ".csv")
        //    {
        //        return new CsvFileReader();
        //    }

        //    else if (extension == ".json")
        //    {
        //        return new JsonFileReader();
        //    }
        //    else
        //    {
        //        throw new KlantenSimulatorException($"No reader available for {fileName}"); //unit
        //    }
        //}

        //public static INameReader GetNameReader(string folder, string fileName, string country, NameType nameType)
        //{
        //    string extension = Path.GetExtension(fileName).ToLower();

        //    if (extension == ".csv")
        //    {
        //        return new CsvFileReader();
        //    }

        //    else if (extension == ".json")
        //    {
        //        return new JsonFileReader();
        //    }

        //    else if (extension == ".txt" && country!= "Switserland")
        //    {
        //        return new TextFileReader();
        //    }

        //    return new TextNameByGenderFileReader();
        //}
        public static ICountryReader GetCountryReader(Countries country)
        {
            switch (country)
            {
                case Countries.Belgium:
                    break;
                case Countries.Denmark:
                    break;
                case Countries.Finland:
                    break;
                case Countries.Poland:
                    break;
                case Countries.Spain:
                    break;
                case Countries.CzechRepublic:
                    break;
                case Countries.Sweden:
                    break;
                case Countries.Switserland:
                    break;
                default:
                    throw new InvalidOperationException("Unknown country");
            }
            return null;

        }

    }

}

