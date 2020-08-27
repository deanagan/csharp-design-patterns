using Xunit;
using Moq;
using FluentAssertions;

namespace Strategy.Test
{
    public class StrategyShould
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

        [Fact]
        public void ReturnMemberDiscountedPrice_WhenMemberWithValidCouponForRegularItem()
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

        [Fact]
        public void ReturnNonMemberDiscountedPrice_WhenNonMemberWithValidCouponForRegularItem()
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

        [Fact]
        public void ReturnBaseMemberDiscountOnly_WhenMemberUsesExpiredCoupon()
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

        [Fact]
        public void ReturnNoDiscount_WhenNonMemberUsesExpiredCoupon()
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

        [Fact]
        public void ReturnBaseMemberDiscountedPrice_WhenMemberUsesValidCouponForProductAlreadyOnSale()
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

        [Fact]
        public void ReturnSalePriceOnly_WhenNonMemberUsesValidCouponForProductAlreadyOnSale()
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
