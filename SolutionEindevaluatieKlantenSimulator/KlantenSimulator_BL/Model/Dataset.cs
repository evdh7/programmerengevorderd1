namespace KlantenSimulatorBL.Model
{
    public class Dataset(string desciption, DateTime dateImported)
    {
        public string Description { get; set; } = desciption;
        public DateTime DateImported { get; set; } = dateImported;
    
    public override string ToString()
        {
            return $"{Description} {DateImported}";
        }
    }
}
