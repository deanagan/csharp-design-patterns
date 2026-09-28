using System;
using System.Collections.Generic;
using AbstractFactory;
using Shouldly;
using Xunit;

namespace AbstractFactoryTest;

public class AbstractFactoryShould
{
    public static TheoryData<string, string, decimal> FactoriesExpectations
    {
        get
        {
            var data = new TheoryData<string, string, decimal>
            {
                { "Corporate", "Corporate", 1000000m },
                { "Small Business", "Small Business", 50000m }
            };
            
            return data;
        }
    }

    [Theory]
    [MemberData(nameof(FactoriesExpectations))]
    public void HaveCorrectGuarantee_WhenUsingFactory(string factoryType, string name, decimal amount)
    {
        // Act
        ICommercialBankingFactory factory = factoryType switch
        {
            "Corporate" => new CorporateBankingFactory(),
            "Small Business" => new SmallBusinessBankingFactory(),
            _ => throw new ArgumentException("Invalid factory type"),
        };
        var guarantee = factory.CreateBankingGuarantee();

        // Assert
        guarantee.ShouldSatisfyAllConditions(
            "guarantee",
            () => guarantee.GetDescription().ShouldBe(name),
            () => guarantee.GetGuaranteeAmount().ShouldBe(amount)
        );
    }

    // public static IEnumerable<object[]> FactoriesAndStorageExpectations
    // {
    //     get
    //     {
    //         yield return new object[] { (new LenovoPartsFactory()), "hdd", 50 };
    //         yield return new object[] { (new DellPartsFactory()), "ssd", 250 };
    //     }
    // }

    // [Theory]
    // [MemberData(nameof(FactoriesAndStorageExpectations))]
    // public void HaveCorrectStorage_WhenUsingFactory(ILaptopPartsFactory factory, string hwtype, int speed)
    // {
    //     // Act
    //     var storage = factory.CreateStorage();

    //     // Assert
    //     using (new FluentAssertions.Execution.AssertionScope("storage"))
    //     {
    //         storage.HardwareType().Should().Be(hwtype);
    //         storage.ReadSpeedInMBytesPerSec().Should().Be(speed);
    //     }
    // }

    // public static IEnumerable<object[]> FactoriesType
    // {
    //     get
    //     {
    //         yield return new object[] { (new LenovoPartsFactory()) };
    //         yield return new object[] { (new DellPartsFactory()) };
    //     }
    // }

    // [Theory]
    // [MemberData(nameof(FactoriesType))]
    // public void ImplementIStorage_WhenUsingFactory(ILaptopPartsFactory factory)
    // {
    //     // Act
    //     var storage = factory.CreateStorage();

    //     // Assert
    //     storage.GetType().Should().Implement<IStorage>();
    // }

    // [Theory]
    // [MemberData(nameof(FactoriesType))]
    // public void ImplementIProcessor_WhenUsingFactory(ILaptopPartsFactory factory)
    // {
    //     // Act
    //     var processor = factory.CreateProcessor();

    //     // Assert
    //     processor.GetType().Should().Implement<IProcessor>();
    // }
}
