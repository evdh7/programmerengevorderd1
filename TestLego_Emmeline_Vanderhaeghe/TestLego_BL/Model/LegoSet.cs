using System.Globalization;

public class LegoSet
{
    public LegoSet(string id, string name, int year, int pieces, int miniFigs, int? minAge, string imageUrl, double? retailPrice)
    {
        Id = id;
        Name = name;
        Year = year;
        Pieces = pieces;
        MiniFigs = miniFigs;
        MinAge = (int)minAge;
        ImageUrl = imageUrl;
        RetailPrice = (double)retailPrice;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
    public int Pieces { get; set; }
    public int MiniFigs { get; set; }
    public int MinAge { get; set; }
    public string ImageUrl { get; set; }

    private double retailPrice;
    public double RetailPrice
    {
        get { return retailPrice; }
        set
        {
            if (value <= 0)
            {
                throw new LegoException(ToString());
            }
            else
            {
                retailPrice = value;
            }
        }
    }
    public override string ToString()
    {
        return $"{Id},{Name},{Year},{Pieces},{MiniFigs},{MinAge},{RetailPrice.ToString(CultureInfo.InvariantCulture) ?? "null"},{ImageUrl}";
    }
}