using RedoLegoTest_BL;
using RedoLegoTest_BL.Model;

namespace RedoLegoTest_UT
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(0.1)]
        [InlineData(100)]
        [InlineData(null)]


        public void Test_Price_Valid(double price)
        {
            LegoSet legoSet = new("1", "Small house set", 1970, 50, 5, 6, "stringurl", 25);
            legoSet.RetailPrice = price;
            Assert.Equal(price, legoSet.RetailPrice);

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void Test_Price_Invalid(double price)
        {
            LegoSet legoSet = new("1", "Small house set", 1970, 50, 5, 6, "stringurl", 25);
            Assert.Throws<LegoException>(() => legoSet.RetailPrice = price);

        }

        [Fact]
        public void Test_AddLegoSet_Valid()
        {
            LegoTheme legoTheme = new("theme1");
            LegoSet legoSet = new("1", "Small house set", 1970, 50, 5, 6, "stringurl", 25);
            LegoSet legoSet2 = new("10", "Small house set", 1970, 50, 5, 6, "stringurl", 25);

            legoTheme.AddLegoSet(legoSet);
            legoTheme.AddLegoSet(legoSet2);
            Assert.Contains(legoSet, legoTheme.LegoSets);
            Assert.Contains(legoSet2, legoTheme.LegoSets);
            Assert.Equal(2, legoTheme.LegoSets.Count);

        }

        [Fact]
        public void Test_AddLegoSet_Invalid()
        {
            LegoTheme legoTheme = new("theme1");
            LegoSet legoSet1 = new("1", "Small house set", 1970, 50, 5, 6, "stringurl", 25);
            LegoSet legoSet2 = new("10", "Small house set", 1970, 50, 5, 6, "stringurl", 25);
            LegoSet legoSet3 = new("10", "Small house set", 1970, 50, 5, 6, "stringurl", 25);

            legoTheme.AddLegoSet(legoSet1);
            legoTheme.AddLegoSet(legoSet2);
            Assert.Throws<LegoException>(() => legoTheme.AddLegoSet(legoSet3));
            Assert.Contains(legoSet1, legoTheme.LegoSets);
            Assert.Contains(legoSet2, legoTheme.LegoSets);
            Assert.Equal(2,legoTheme.LegoSets.Count);

        }

    }
}