using KlantenSimulatorBL.Interfaces;
using System.Text;

namespace KlantenSimulatorDL_File.FileReaders
{
    public class ReaderLayout : INameReaderConfig
    {
        public ReaderLayout(uint nameColumn, uint frequencyColumn, Encoding? forcedEncoding)
        {
            NameColumn = nameColumn;
            FrequencyColumn = frequencyColumn;
            ForcedEncoding = forcedEncoding;
        }

        private uint NameColumn { get; set; }
        private uint FrequencyColumn { get; set; }
        public Encoding? ForcedEncoding { get; set; }

        public Encoding? GetEncoding()
        {
            return ForcedEncoding;
        }

        public uint GetFrequencyColumn()
        {
            return FrequencyColumn;
        }

        public uint GetNameColumn()
        {
            return NameColumn;
        }
    }
}
