using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDL_File;
using System.Diagnostics;

namespace KlantenSimulatorUtils
{
    public static class KlantenSimulatorFileReaderFactory
    {
        public static IFileReader GetCvsFileReader()
        {
            return new CvsFileReader();
        }

    }
}
