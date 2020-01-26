using NUnit.Framework;
using Moq;

namespace Strategy.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGetDiscountIfMemberBoxingDayCouponValidRegularItem_ShouldReturnMemberDiscountedPrice()
        {
            Assert.Fail();
        }

        [Test]
        public void TestGetDiscountIfNonMemberBoxingDayCouponValidRegularItem_ShouldReturnNonMemberDiscountedPrice()
        {
            Assert.Fail();
        }

        [Test]
        public void TestGetDiscountIfMemberBoxingDayCouponExpired_ShouldReturnNoMemberDiscountedPrice()
        {
            Assert.Fail();
        }

        [Test]
        public void TestGetDiscountIfNonMemberBoxingDayCouponExpired_ShouldReturnNoNonMemberDiscountedPrice()
        {
            Assert.Fail();
        }

        [Test]
        public void TestGetDiscountIfMemberBoxingDayCouponValidOnProductAlreadyOnSale_ShouldReturnNoMemberDiscountedPrice()
        {
            Assert.Fail();
        }

        [Test]
        public void TestGetDiscountIfNonMemberBoxingDayCouponValidOnProductAlreadyOnSale_ShouldReturnNoNonMemberDiscountedPrice()
        {
            Assert.Fail();
        }
    }
}