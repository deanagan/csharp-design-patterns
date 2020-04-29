using System;
using Xunit;
using Moq;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;

namespace Builder.Test
{
    public class BuilderShould
    {
        private List<Product> _groceryShoppingCart;

        public BuilderShould()
        {
            _groceryShoppingCart = new List<Product>
            {
                new Product {
                    Name = "Heinz Baked Beans",
                    StockKeepingUnit = "Food-1234-5385",
                    RegularRetailPrice = 4.00M
                },
                new Product {
                    Name = "Toothpaste",
                    StockKeepingUnit = "Hygiene-1254-5283",
                    RegularRetailPrice = 1.50M
                },
                new Product {
                    Name = "Organic Frozen Berries",
                    StockKeepingUnit = "Food-3494-0534",
                    RegularRetailPrice = 6.00M
                }

            };
        }

        [Fact]
        public void TotalPriceIsCorrect_WhenUsingBuiltStrategy()
        {
            // Arrange
            var strategy = new SkuCodeStartDiscountStrategyBuilder()
                            .WithDiscountInPercentage(5)
                            .ApplicableToSKUCode("Food")
                            .Build();
            // Act
            var totalAmount = _groceryShoppingCart
                            .Select(product => strategy.CalculateDiscountedRetailPrice(product))
                            .Sum();

            // Assert
            totalAmount.Should().BeApproximately(11.00M, 0.001M);

        }
    }
}
