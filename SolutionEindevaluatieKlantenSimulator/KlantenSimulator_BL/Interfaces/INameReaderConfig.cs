using System.Text;

namespace KlantenSimulatorBL.Interfaces
{ 
    public interface INameReaderConfig
    {
        public uint GetNameColumn();
        public uint GetFrequencyColumn();
        public void SetFrequencyColumn(uint column);
        public Encoding? GetEncoding();
        public string GetSearchString();
    }
}

