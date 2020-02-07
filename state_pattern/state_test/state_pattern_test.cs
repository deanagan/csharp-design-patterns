using System;
using NUnit.Framework;
using Moq;
using FluentAssertions;
using State;

namespace State.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ProductPrice_IsDiscounted_WhenCoupon_IsValid()
        {
            // Arrange
            var product = Mock.Of<IProduct> ( p => p.Price == 100.0M);
            var expiry = DateTime.Today.AddDays(2);
            var coupon = new Coupon(5, expiry);       
            
            // Assert
            coupon.FinalSellingPrice(product).Should().Be(95.0M);
        }

        [Test]
        public void ProductPrice_IsSame_WhenCoupon_IsExpired()
        {
            // Arrange
            var product = Mock.Of<IProduct> ( p => p.Price == 100.0M);
            var expiry = DateTime.Today.AddDays(2);
            var coupon = new Coupon(5, expiry);

            // Act
            coupon.UpdateExpiryDate(expiry.AddDays(-4));

            // Assert
            coupon.FinalSellingPrice(product).Should().Be(100.0M);
        }

        [Test]
        public void ArgumentException_IsThrown_WhenCouponConstructedAsExpired()
        {
            // Arrange
            var product = Mock.Of<IProduct> ( p => p.Price == 100.0M);
            var expiry = DateTime.Today.AddDays(-2);
            
            // Act
            Action act = () => new Coupon(5, expiry);

            // Assert
            act.Should().ThrowExactly<ArgumentException>();
        }
    }
}