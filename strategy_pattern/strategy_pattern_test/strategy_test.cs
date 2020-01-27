using NUnit.Framework;
using Moq;

namespace Strategy.Test
{
    public class Tests
    {
        private ICoupon _mockCoupon;
        private IProduct _mockProduct;
        
        ICoupon CreateMockCoupon(bool isExpired, int discountPercentage)
        {
            return Mock.Of<ICoupon> 
            (
                coupon => 
                coupon.IsExpired() == isExpired &&
                coupon.DiscountPercentage() == discountPercentage
            );
        }

        IProduct CreateMockProduct(decimal sellingPrice, bool isOnSale)
        {
            return Mock.Of<IProduct>
            (
                product =>
                product.SellingPrice() == sellingPrice &&
                product.IsOnSale() == isOnSale
            );
        }
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGetDiscountIfMemberCouponValidRegularItem_ShouldReturnMemberDiscountedPrice()
        {
            // Arrange
            _mockCoupon = CreateMockCoupon(false, 5);
            _mockProduct = CreateMockProduct(100M, false);

            // Act
            var sut = new Member(_mockCoupon);

            // Assert
            Assert.AreEqual(90M, sut.Price(_mockProduct));
        }

        [Test]
        public void TestGetDiscountIfNonMemberCouponValidRegularItem_ShouldReturnNonMemberDiscountedPrice()
        {
            // Arrange
           _mockCoupon = CreateMockCoupon(false, 5);
           _mockProduct = CreateMockProduct(100M, false);

            // Act
            var sut = new NonMember(_mockCoupon);

            // Assert
            Assert.AreEqual(95M, sut.Price(_mockProduct));
        }

        [Test]
        public void TestGetDiscountIfMemberCouponExpired_ShouldReturnNoCouponDiscountedPrice()
        {
            // Arrange
            _mockCoupon = CreateMockCoupon(true, 5);
            _mockProduct = CreateMockProduct(100M, false);
            
            // Act
            var sut = new Member(_mockCoupon);

            // Assert
            Assert.AreEqual(95M, sut.Price(_mockProduct));
        }

        [Test]
        public void TestGetDiscountIfNonMemberCouponExpired_ShouldReturnNoNonMemberDiscountedPrice()
        {
            // Arrange
            _mockCoupon = CreateMockCoupon(true, 5);
            _mockProduct = CreateMockProduct(100M, false);
            
            // Act
            var sut = new NonMember(_mockCoupon);

            // Assert
            Assert.AreEqual(100M, sut.Price(_mockProduct));
        }

        [Test]
        public void TestGetDiscountIfMemberCouponValidOnProductAlreadyOnSale_ShouldReturnBaseMemberDiscountedAddedToSalePrice()
        {
            // Arrange
            _mockCoupon = CreateMockCoupon(false, 5);
            _mockProduct = CreateMockProduct(100M, true);
            
            // Act
            var sut = new Member(_mockCoupon);

            // Assert
            Assert.AreEqual(90M, sut.Price(_mockProduct));
        }

        [Test]
        public void TestGetDiscountIfNonMemberCouponValidOnProductAlreadyOnSale_ShouldReturnSalePrice()
        {
            // Arrange
            _mockCoupon = CreateMockCoupon(false, 5);
            _mockProduct = CreateMockProduct(97M, true);
          
            // Act
            var sut = new NonMember(_mockCoupon);

            // Assert
            Assert.AreEqual(97M, sut.Price(_mockProduct));
        }
    }
}