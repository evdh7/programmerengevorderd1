using System.Threading.Tasks.Dataflow;

namespace KlantenSimulatorBL.Model
{
    public class Dataset(int datasetId, string desciption, DateTime dateImported)
    {
        public int DatasetId { get; set; } = datasetId;
        public string Description { get; set; } = desciption;
        public DateTime DateImported { get; set; } = dateImported;
    
    public override string ToString()
        {
            return $"{Description} {DateImported}";
        }
    }
}
