using System;
using NUnit.Framework;
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

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetBasicPerks_WhenMilesEarnedIsLessThanSilver()
        {
            // Arrange
            var perksProducer = new PerksProducer(_thresholds);

            // Act
            var perks = perksProducer.GetPerks(8000);

            // Assert
            perks.Should().BeOfType<BasicPerks>();
        }

        [Test]
        public void TotalMiles_WhenUsingBasicPerks()
        {
            // Arrange
            var perksProducer = new PerksProducer(_thresholds);
            var calc = new EarningBonusCalculator(perksProducer);

            // Act
            var totalMiles = calc.UpdatedMiles(8000, 100);

            // Assert
            totalMiles.Should().Be(8100);
        }

        [Test]
        public void GetSilverPerks_WhenMilesEarnedIsAtSilverThreshold()
        {
            // Arrange
            var perksProducer = new PerksProducer(_thresholds);

            // Act
            var perks = perksProducer.GetPerks(10000);

            // Assert
            perks.Should().BeOfType<SilverPerks>();
        }

        [Test]
        public void AddingMilesAtSilverPerks_AddsSilverPerkMilesEarned()
        {
            // Arrange
            var perksProducer = new PerksProducer(_thresholds);
            var calc = new EarningBonusCalculator(perksProducer);

            // Act
            var totalMiles = calc.UpdatedMiles(10000, 2000);

            // Assert
            totalMiles.Should().Be(13000);
        }

        [Test]
        public void GetGoldPerks_WhenMilesEarnedIsAtGoldThreshold()
        {
            // Arrange
            var perksProducer = new PerksProducer(_thresholds);

            // Act
            var perks = perksProducer.GetPerks(20001);

            // Assert
            perks.Should().BeOfType<GoldPerks>();
        }

        [Test]
        public void AddingMilesAtGoldPerks_AddsGoldPerkMilesEarned()
        {
           // Arrange
            var perksProducer = new PerksProducer(_thresholds);
            var calc = new EarningBonusCalculator(perksProducer);

            // Act
            var totalMiles = calc.UpdatedMiles(20000, 2000);

            // Assert
            totalMiles.Should().Be(24000);
        }
    }
}
