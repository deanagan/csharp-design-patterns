using AbstractFactory;
using Shouldly;

namespace AbstractFactoryTest;

public class AbstractFactoryShould
{
    public static TheoryData<string, string, decimal, string, string, decimal> FactoriesExpectations
    {
        get
        {
            var data = new TheoryData<string, string, decimal, string, string, decimal>
            {
                {
                    "Corporate",
                    "Corporate Bank Guarantee", 1000000m, "Corporate Client",
                    "Corporate Business overdraft facility", 100000m
                },
                {
                    "Small Business",
                    "Small Business Bank Guarantee", 50000m, "Small Business Beneficiary",
                    "Small Business overdraft facility", 10000m
                }
            };
            
            return data;
        }
    }

    [Theory]
    [MemberData(nameof(FactoriesExpectations))]
    public void CreateMatchingProducts_WhenUsingFactory(
        string factoryType,
        string guaranteeDescription,
        decimal guaranteeAmount,
        string beneficiary,
        string overdraftDescription,
        decimal overdraftLimit)
    {
        ICommercialBankingFactory factory = factoryType switch
        {
            "Corporate" => new CorporateBankingFactory(),
            "Small Business" => new SmallBusinessBankingFactory(),
            _ => throw new ArgumentException("Unknown factory type", nameof(factoryType))
        };
        var guarantee = factory.CreateBankingGuarantee();
        var overdraft = factory.CreateBusinessOverdraft();

        guarantee.ShouldSatisfyAllConditions(
            "guarantee",
            () => guarantee.GetDescription().ShouldBe(guaranteeDescription),
            () => guarantee.GetGuaranteeAmount().ShouldBe(guaranteeAmount),
            () => guarantee.GetBeneficiary().ShouldBe(beneficiary)
        );

        overdraft.ShouldSatisfyAllConditions(
            "overdraft",
            () => overdraft.GetDescription().ShouldBe(overdraftDescription),
            () => overdraft.GetOverdraftLimit().ShouldBe(overdraftLimit)
        );
    }
}
