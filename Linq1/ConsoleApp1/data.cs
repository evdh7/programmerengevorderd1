namespace Linq
{
    public class Data
    {

        public string provincie;
        public string gemeente;
        public string straatnaam;

        public Data(string provincie, string gemeente, string straatnaam)
        {
            this.provincie = provincie;
            this.gemeente = gemeente;
            this.straatnaam = straatnaam;
        }
        public override string ToString()
        {
            return $"{provincie}{gemeente}{straatnaam}";
        }
    }
}