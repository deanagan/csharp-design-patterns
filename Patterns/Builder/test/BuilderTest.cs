using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;

namespace Builder.Test
{
    public class BuilderShould
    {
        private List<Product> _groceryShoppingCart;
        private Fixture _fixture = new Fixture();
        public BuilderShould()
        {

            _groceryShoppingCart = new List<Product>
            {
                new Product {
                    Name = _fixture.Create<string>(),
                    StockKeepingUnit = "Food-" + _fixture.Create<string>(),
                    RegularRetailPrice = 4.00M
                },
                new Product {
                    Name = _fixture.Create<string>(),
                    StockKeepingUnit = "Hygiene-" + _fixture.Create<string>(),
                    RegularRetailPrice = 1.50M
                },
                new Product {
                    Name = _fixture.Create<string>(),
                    StockKeepingUnit = "Food-" + _fixture.Create<string>(),
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
