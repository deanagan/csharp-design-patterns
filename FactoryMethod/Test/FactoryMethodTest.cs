using System;
using Xunit;
using System.Collections.Generic;
using Moq;
using FluentAssertions;

namespace FactoryMethod.Test
{
    public class FactoryMethodShould
    {
        private const int GoldPerksThreshold = 20000;
        private const int SilverPerksThreshold = 10000;
        private List<(int, string)> _thresholds = new List<(int, string)> {
                ( GoldPerksThreshold, "GoldPerks"),
                ( SilverPerksThreshold, "SilverPerks")
            };

        [Fact]
        public void ReturnBasicPerksType_WhenMilesEarnedIsLessThanSilver()
        {
            // Arrange
            var perksFactory = new PerksFactory(_thresholds);

            // Act
            var perks = perksFactory.GetPerks(8000);

            // Assert
            perks.Should().BeOfType<BasicPerks>();
        }

        [Fact]
        public void HaveCorrectTotalMiles_WhenUsingBasicPerks()
        {
            // Arrange
            var perksFactory = new PerksFactory(_thresholds);
            var calc = new EarningBonusCalculator(perksFactory);

            // Act
            var totalMiles = calc.UpdatedMiles(8000, 100);

            // Assert
            totalMiles.Should().Be(8100);
        }

        [Fact]
        public void GetSilverPerks_WhenMilesEarnedIsAtSilverThreshold()
        {
            // Arrange
            var perksFactory = new PerksFactory(_thresholds);

            // Act
            var perks = perksFactory.GetPerks(10000);

            // Assert
            perks.Should().BeOfType<SilverPerks>();
        }

        [Fact]
        public void AddingMilesAtSilverPerks_AddsSilverPerkMilesEarned()
        {
            // Arrange
            var perksFactory = new PerksFactory(_thresholds);
            var calc = new EarningBonusCalculator(perksFactory);

            // Act
            var totalMiles = calc.UpdatedMiles(10000, 2000);

            // Assert
            totalMiles.Should().Be(13000);
        }

        [Fact]
        public void GetGoldPerks_WhenMilesEarnedIsAtGoldThreshold()
        {
            // Arrange
            var perksFactory = new PerksFactory(_thresholds);

            // Act
            var perks = perksFactory.GetPerks(20001);

            // Assert
            perks.Should().BeOfType<GoldPerks>();
        }

        [Fact]
        public void AddingMilesAtGoldPerks_AddsGoldPerkMilesEarned()
        {
           // Arrange
            var perksFactory = new PerksFactory(_thresholds);
            var calc = new EarningBonusCalculator(perksFactory);

            // Act
            var totalMiles = calc.UpdatedMiles(20000, 2000);

            // Assert
            totalMiles.Should().Be(24000);
        }
    }
}
