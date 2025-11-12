using System.Globalization;
using RedoLegoTest_BL;

namespace RedoLegoTest_BL.Model
{
    public class LegoSet
    {
        public LegoSet(string id, string name, int year, int pieces, int miniFigs, int? minAge, string imageUrl, double? retailPrice)
        {
            Id = id;
            Name = name;
            Year = year;
            Pieces = pieces;
            MiniFigs = miniFigs;
            MinAge = minAge;
            ImageUrl = imageUrl;
            RetailPrice = retailPrice;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int Pieces { get; set; }
        public int MiniFigs { get; set; }
        public int? MinAge { get; set; }
        public string ImageUrl { get; set; }

        private double? retailPrice;
        public double? RetailPrice
        {
            get
            {
                return retailPrice;
            }
            set
            {
                if (value is not null && value<=0) { throw new LegoException(); } else { retailPrice = value; }
            }
        }

        public override string ToString()
        {
            return $"{Id},{Name},{Year},{Pieces},{MiniFigs},{MinAge},{RetailPrice?.ToString(CultureInfo.InvariantCulture) ?? "null"},{ImageUrl}";
        }

        public override bool Equals(object? obj)
        {
            return obj is LegoSet set &&
                   Id == set.Id;
        }
    }
}