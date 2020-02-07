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
            var coupon = new Coupon(5);

            // Act
            coupon.UpdateExpiryDate(DateTime.Today.AddDays(1));

            // Assert
            coupon.FinalSellingPrice(product).Should().Be(95.0M);
        }

        [Test]
        public void ProductPrice_IsSame_WhenCoupon_IsExpired()
        {
            Assert.Fail();
        }
    }
}