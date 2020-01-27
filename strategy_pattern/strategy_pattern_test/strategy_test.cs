using NUnit.Framework;
using Moq;

namespace Strategy.Test
{
    public class Tests
    {

        private Mock<ICoupon> _coupon;
        private Mock<IProduct> _product;
        [SetUp]
        public void Setup()
        {
            _coupon = new Mock<ICoupon>();
            _product = new Mock<IProduct>();
        }

        [Test]
        public void TestGetDiscountIfMemberCouponValidRegularItem_ShouldReturnMemberDiscountedPrice()
        {
            // Arrange
            _coupon.Setup(c => c.IsExpired()).Returns(false);
            _coupon.Setup(c => c.DiscountPercentage()).Returns(5);

            _product.Setup(p => p.SellingPrice()).Returns(100M);
            _product.Setup(p => p.OnSale()).Returns(false);

            // Act
            var sut = new Member(_coupon.Object);

            // Assert
            Assert.AreEqual(90M, sut.Price(_product.Object));
        }

        [Test]
        public void TestGetDiscountIfNonMemberCouponValidRegularItem_ShouldReturnNonMemberDiscountedPrice()
        {
            // Arrange
            _coupon.Setup(c => c.IsExpired()).Returns(false);
            _coupon.Setup(c => c.DiscountPercentage()).Returns(5);

            _product.Setup(p => p.SellingPrice()).Returns(100M);
            _product.Setup(p => p.OnSale()).Returns(false);

            // Act
            var sut = new NonMember(_coupon.Object);

            // Assert
            Assert.AreEqual(95M, sut.Price(_product.Object));
        }

        [Test]
        public void TestGetDiscountIfMemberCouponExpired_ShouldReturnNoCouponDiscountedPrice()
        {
            // Arrange
            _coupon.Setup(c => c.IsExpired()).Returns(true);
            _coupon.Setup(c => c.DiscountPercentage()).Returns(5);

            _product.Setup(p => p.SellingPrice()).Returns(100M);
            _product.Setup(p => p.OnSale()).Returns(false);
            
            // Act
            var sut = new Member(_coupon.Object);

            // Assert
            Assert.AreEqual(95M, sut.Price(_product.Object));
        }

        [Test]
        public void TestGetDiscountIfNonMemberCouponExpired_ShouldReturnNoNonMemberDiscountedPrice()
        {
            // Arrange
            _coupon.Setup(c => c.IsExpired()).Returns(true);
            _coupon.Setup(c => c.DiscountPercentage()).Returns(5);

            _product.Setup(p => p.SellingPrice()).Returns(100M);
            _product.Setup(p => p.OnSale()).Returns(false);
            
            // Act
            var sut = new NonMember(_coupon.Object);

            // Assert
            Assert.AreEqual(100M, sut.Price(_product.Object));
        }

        [Test]
        public void TestGetDiscountIfMemberCouponValidOnProductAlreadyOnSale_ShouldReturnBaseMemberDiscountedAddedToSalePrice()
        {
            // Arrange
            _coupon.Setup(c => c.IsExpired()).Returns(false);
            _coupon.Setup(c => c.DiscountPercentage()).Returns(5);

            _product.Setup(p => p.SellingPrice()).Returns(100M);
            _product.Setup(p => p.OnSale()).Returns(true);
            
            // Act
            var sut = new Member(_coupon.Object);

            // Assert
            Assert.AreEqual(90M, sut.Price(_product.Object));
        }

        [Test]
        public void TestGetDiscountIfNonMemberCouponValidOnProductAlreadyOnSale_ShouldReturnSalePrice()
        {
            // Arrange
            _coupon.Setup(c => c.IsExpired()).Returns(false);
            _coupon.Setup(c => c.DiscountPercentage()).Returns(5);

            _product.Setup(p => p.SellingPrice()).Returns(97M);
            _product.Setup(p => p.OnSale()).Returns(true);
            
            // Act
            var sut = new NonMember(_coupon.Object);

            // Assert
            Assert.AreEqual(97M, sut.Price(_product.Object));
        }
    }
}