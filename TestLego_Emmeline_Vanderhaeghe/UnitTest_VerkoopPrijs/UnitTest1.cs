using System.Diagnostics;
using TestLego_BL;

namespace UnitTest_VerkoopPrijs
{
    public class UnitTest1
    {
        [Fact]
        public void VerkoopPrijs_Valid()
        {
            double setPrice = 2;
            LegoSet legoSet = new("1", "Small house set", 1970, 50, 5, 6, "stringurl", setPrice);
            Assert.Equal(setPrice, legoSet.RetailPrice);
        }

        [Fact]
        public void VerkoopPrijs_Invalid()
        {
            double setPrice = -2;
            LegoSet legoSet = new("1", "Small house set", 1970, 50, 5, 6, "stringurl", setPrice);
            Assert.Throws<LegoException>(()=>
                legoSet.RetailPrice = default);
        }
    }
}