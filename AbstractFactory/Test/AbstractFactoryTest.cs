using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

namespace Laptop.Test
{
    public class AbstractFactoryShould
    {
        public static IEnumerable<object[]> FactoriesAndExpectations
        {
            get
            {
                yield return new object[] { (new LenovoPartsFactory()), "AMD", 2.1 };
                yield return new object[] { (new DellPartsFactory()), "Intel", 1.8 };
            }
        }

        [Theory]
        [MemberData(nameof(FactoriesAndExpectations))]
        public void HaveCorrectSpecification_WhenUsingFactory(ILaptopPartsFactory factory, string name, double speed)
        {
            // Act
            var processor = factory.CreateProcessor();

            // Assert
            processor.BrandName().Should().Be(name);
            processor.SpeedInGigaHertz().Should().Be(speed);
        }

    }
}
