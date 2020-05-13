using NUnit.Framework;
using Moq;
using FluentAssertions;

namespace Strategy.Test
{
    public class Tests
    {
        private delegate decimal PriceCalc(IProduct product);
        private ICoupon _mockCoupon;
        private IProduct _mockProduct;
        private IDiscountScheme _discountScheme;
        
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
        public void DiscountIfMember_WithValidCoupon_ForRegularItem_ShouldReturnMemberDiscountedPrice()
        {
            // Arrange
            _mockCoupon = CreateMockCoupon(false, 5);
            _mockProduct = CreateMockProduct(100M, false);
            _discountScheme = new MemberDiscountScheme();

            // Act
            var price = _discountScheme.ComputePrice(_mockProduct, _mockCoupon);
            
            // Assert
            price.Should().Be(90M);
        }

        [Test]
        public void DiscountIfNonMember_WithValidCoupon_ForRegularItem_ShouldReturnNonMemberDiscountedPrice()
        {
            // Arrange
           _mockCoupon = CreateMockCoupon(false, 5);
           _mockProduct = CreateMockProduct(100M, false);
           _discountScheme = new NonMemberDiscountScheme();

            // Act
            var price = _discountScheme.ComputePrice(_mockProduct, _mockCoupon);

            // Assert
            price.Should().Be(95M);
        }

        [Test]
        public void DiscountIfMember_WithCouponExpired_ShouldReturnBaseMemberDiscountOnly()
        {
            // Arrange
            _mockCoupon = CreateMockCoupon(true, 5);
            _mockProduct = CreateMockProduct(100M, false);
            _discountScheme = new MemberDiscountScheme();
            
            // Act
            var price = _discountScheme.ComputePrice(_mockProduct, _mockCoupon);

            // Assert
            price.Should().Be(95M);
        }

        [Test]
        public void DiscountIfNonMember_WithCouponExpired_ShouldReturnNoDiscount()
        {
            // Arrange
            _mockCoupon = CreateMockCoupon(true, 5);
            _mockProduct = CreateMockProduct(100M, false);
            _discountScheme = new NonMemberDiscountScheme();
            
            // Act
            var price = _discountScheme.ComputePrice(_mockProduct, _mockCoupon);

            // Assert
            price.Should().Be(100M);
        }

        [Test]
        public void DiscountIfMember_WithValidCoupon_ForProductAlreadyOnSale_ShouldReturnBaseMemberDiscountedAddedToSalePrice()
        {
            // Arrange
            _mockCoupon = CreateMockCoupon(false, 5);
            _mockProduct = CreateMockProduct(100M, true);
            _discountScheme = new MemberDiscountScheme();
            
            // Act
            var price = _discountScheme.ComputePrice(_mockProduct, _mockCoupon);

            // Assert
            price.Should().Be(90M);
        }

        [Test]
        public void DiscountIfNonMember_WithValidCoupon_ForProductAlreadyOnSale_ShouldReturnSalePriceOnly()
        {
            // Arrange
            _mockCoupon = CreateMockCoupon(false, 5);
            _mockProduct = CreateMockProduct(97M, true);
            _discountScheme = new NonMemberDiscountScheme();
          
            // Act
            var price = _discountScheme.ComputePrice(_mockProduct, _mockCoupon);

            // Assert
            price.Should().Be(97M);
        }
    }
}