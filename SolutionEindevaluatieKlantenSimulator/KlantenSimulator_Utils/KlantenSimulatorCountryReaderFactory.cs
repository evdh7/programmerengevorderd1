using KlantenSimulatorBL.Enums;
using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDL_File.CountryReaders;
using KlantenSimulatorDL_File.FileReaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorUtils
{
    public class KlantenSimulatorCountryReaderFactory
    {
        public static ICountryReader GetCountryReader(Countries country)
        {

            return country switch
            {
                Countries.Belgium => new BelgiumCountryReader(),
                Countries.Denmark => new DenmarkCountryReader(),
                Countries.Finland => new FinlandCountryReader(),
                //Countries.Poland => new PolandCountryReader(),
                Countries.Spain => new SpainCountryReader(),
                //Countries.CzechRepublic => new CzechCountryReader(),
                Countries.Sweden => new SwedenCountryReader(),
                Countries.Switserland => new SwitserlandCountryReader(),
                _ => throw new InvalidOperationException("Unknown country")
            };

        }
    }
}
