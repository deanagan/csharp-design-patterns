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
        public void TestGetDiscountIfMemberCouponValidRegularItem_ShouldReturnMemberDiscountedPrice()
        {
            // Arrange
            var coupon = new Mock<ICoupon>();
            coupon.Setup(c => c.IsExpired()).Returns(false);
            coupon.Setup(c => c.Discount()).Returns(0.05);

            var product = new Mock<IProduct>();
            product.Setup(p => p.SellingPrice()).Returns(100.00);
            product.Setup(p => p.OnSale()).Returns(false);
            // Act
            var sut = new Member(coupon.Object);

            // Assert
            Assert.AreEqual(90.00,sut.CalculatePrice(product.Object));
        }

        [Test]
        public void TestGetDiscountIfNonMemberCouponValidRegularItem_ShouldReturnNonMemberDiscountedPrice()
        {
            // Arrange
            var coupon = new Mock<ICoupon>();
            coupon.Setup(c => c.IsExpired()).Returns(false);
            coupon.Setup(c => c.Discount()).Returns(0.05);

            var product = new Mock<IProduct>();
            product.Setup(p => p.SellingPrice()).Returns(100.00);
            product.Setup(p => p.OnSale()).Returns(false);
            // Act
            var sut = new NonMember(coupon.Object);

            // Assert
            Assert.AreEqual(95.00,sut.CalculatePrice(product.Object));
        }

        [Test]
        public void TestGetDiscountIfMemberCouponExpired_ShouldReturnNoCouponDiscountedPrice()
        {
            // Arrange
            var coupon = new Mock<ICoupon>();
            coupon.Setup(c => c.IsExpired()).Returns(true);
            coupon.Setup(c => c.Discount()).Returns(0.05);

            var product = new Mock<IProduct>();
            product.Setup(p => p.SellingPrice()).Returns(100.00);
            product.Setup(p => p.OnSale()).Returns(false);
            
            // Act
            var sut = new Member(coupon.Object);

            // Assert
            Assert.AreEqual(95.00 ,sut.CalculatePrice(product.Object));
        }

        [Test]
        public void TestGetDiscountIfNonMemberCouponExpired_ShouldReturnNoNonMemberDiscountedPrice()
        {
            // Arrange
            var coupon = new Mock<ICoupon>();
            coupon.Setup(c => c.IsExpired()).Returns(true);
            coupon.Setup(c => c.Discount()).Returns(0.05);

            var product = new Mock<IProduct>();
            product.Setup(p => p.SellingPrice()).Returns(100.00);
            product.Setup(p => p.OnSale()).Returns(false);
            
            // Act
            var sut = new NonMember(coupon.Object);

            // Assert
            Assert.AreEqual(100.00 ,sut.CalculatePrice(product.Object));
        }

        [Test]
        public void TestGetDiscountIfMemberCouponValidOnProductAlreadyOnSale_ShouldReturnBaseMemberDiscountedAddedToSalePrice()
        {
            // Arrange
            var coupon = new Mock<ICoupon>();
            coupon.Setup(c => c.IsExpired()).Returns(false);
            coupon.Setup(c => c.Discount()).Returns(0.05);

            var product = new Mock<IProduct>();
            product.Setup(p => p.SellingPrice()).Returns(100.00);
            product.Setup(p => p.OnSale()).Returns(true);
            
            // Act
            var sut = new Member(coupon.Object);

            // Assert
            Assert.AreEqual(90.00 ,sut.CalculatePrice(product.Object));
        }

        [Test]
        public void TestGetDiscountIfNonMemberCouponValidOnProductAlreadyOnSale_ShouldReturnSalePrice()
        {
            // Arrange
            var coupon = new Mock<ICoupon>();
            coupon.Setup(c => c.IsExpired()).Returns(false);
            coupon.Setup(c => c.Discount()).Returns(0.05);

            var product = new Mock<IProduct>();
            product.Setup(p => p.SellingPrice()).Returns(97.00);
            product.Setup(p => p.OnSale()).Returns(true);
            
            // Act
            var sut = new NonMember(coupon.Object);

            // Assert
            Assert.AreEqual(97.00 ,sut.CalculatePrice(product.Object));
        }
    }
}