using KlantenSimulatorBL.Interfaces;
using KlantenSimulatorDL_File;
using System.Diagnostics;

namespace KlantenSimulatorUtils
{
    public static class KlantenSimulatorFileReaderFactory
    {
        public static IFileReader GetFileReader(string fileName)
        {
            return new CvsFileReader();
        }

    }
}
