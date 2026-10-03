using System;
using Xunit;
using Moq;
using FluentAssertions;

namespace State.Test
{
    public class StateShould
    {
        private readonly IProduct _product = Mock.Of<IProduct> ( p => p.Price == 100.0M);
        [Fact]
        public void ReturnDiscountedProductPrice_WhenCoupon_IsValid()
        {
            // Arrange
            var expiry = DateTime.Today.AddDays(2);
            var coupon = new Coupon(5, expiry);
            // Act
            var price = coupon.ApplyDiscountTo(_product);
            // Assert
            price.Should().Be(95.0M);
        }

        [Fact]
        public void ReturnSameProductPrice_WhenCouponIsExpired()
        {
            // Arrange
            var expiry = DateTime.Today.AddDays(2);
            var coupon = new Coupon(5, expiry);
            coupon.UpdateExpiryDate(expiry.AddDays(-4));
            // Act
            var price = coupon.ApplyDiscountTo(_product);
            // Assert
            price.Should().Be(100.0M);
        }

        [Fact]
        public void ThrowArgumentException_WhenCouponConstructedAsExpired()
        {
            // Arrange
            var expiry = DateTime.Today.AddDays(-2);
            // Act
            Action act = () => new Coupon(5, expiry);
            // Assert
            act.Should().ThrowExactly<ArgumentException>();
        }
    }
}
