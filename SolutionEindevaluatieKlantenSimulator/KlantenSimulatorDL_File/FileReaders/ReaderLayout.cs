using KlantenSimulatorBL.Interfaces;
using System.Text;

namespace KlantenSimulatorDL_File.FileReaders
{
    public class ReaderLayout(uint nameColumn, uint frequencyColumn, Encoding? forcedEncoding, string? searchString) : INameReaderConfig
    {
        private uint NameColumn { get; set; } = nameColumn;
        private uint FrequencyColumn { get; set; } = frequencyColumn;
        private Encoding? ForcedEncoding { get; set; } = forcedEncoding;

        private string? SearchString { get; set; } = searchString;

        public Encoding? GetEncoding()
        {
            return ForcedEncoding;
        }

        public uint GetFrequencyColumn()
        {
            return FrequencyColumn;
        }
        public void SetFrequencyColumn(uint column) 
        { 
            FrequencyColumn = column;
        }
        public uint GetNameColumn()
        {
            return NameColumn;
        }

        public string GetSearchString() 
        {
            return SearchString;
        }
    }
}
