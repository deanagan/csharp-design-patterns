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
            // Arrange
            var coupon = new Mock<ICoupon>();
            coupon.Setup(c => c.IsExpired()).Returns(false);
            coupon.Setup(c => c.Discount()).Returns(0.05);

            var product = new Mock<IProduct>();
            product.Setup(p => p.SellingPrice()).Returns(100.00);
            
            // Act
            var sut = new Member(coupon.Object);

            // Assert
            Assert.AreEqual(95.00,sut.CalculatePrice(product.Object));
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