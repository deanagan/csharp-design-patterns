using Flyweight;
using Shouldly;
using Xunit;

namespace FlyweightTest;

public class FlyweightTest
{
    [Fact]
    public void Factory_ReturnsSameFlyweightInstance_ForDuplicateProductCodes()
    {
        // Arrange
        var factory = new LoanProductFlyweightFactory();

        // Act
        var flyweight1 = factory.GetFlyweight("SME-WORKING-CAPITAL");
        var flyweight2 = factory.GetFlyweight("SME-WORKING-CAPITAL");

        // Assert
        flyweight1.ShouldBeSameAs(flyweight2);
    }

    [Fact]
    public void Factory_CreatesDistinctFlyweights_ForDifferentProductCodes()
    {
        // Arrange
        var factory = new LoanProductFlyweightFactory();

        // Act
        var smeFlyweight = factory.GetFlyweight("SME-WORKING-CAPITAL");
        var equipmentFlyweight = factory.GetFlyweight("EQUIPMENT-LEASE");

        // Assert
        smeFlyweight.ShouldNotBeSameAs(equipmentFlyweight);
        smeFlyweight.ProductCode.ShouldBe("SME-WORKING-CAPITAL");
        equipmentFlyweight.ProductCode.ShouldBe("EQUIPMENT-LEASE");
        factory.GetCachedInstanceCount().ShouldBe(2);
    }

    [Fact]
    public void Factory_ThrowsArgumentException_ForUnknownProductCode()
    {
        // Arrange
        var factory = new LoanProductFlyweightFactory();

        // Act & Assert
        var ex = Should.Throw<ArgumentException>(() => factory.GetFlyweight("UNKNOWN-PRODUCT"));
        ex.Message.ShouldContain("Unknown product code: UNKNOWN-PRODUCT");
    }

    [Fact]
    public void ActiveLoanAccount_ComputesInterestAndDisclosure_UsingSharedFlyweight()
    {
        // Arrange
        var factory = new LoanProductFlyweightFactory();
        var template = factory.GetFlyweight("EQUIPMENT-LEASE"); // Base rate factor: 0.060

        var loan = new ActiveLoanAccount(
            productTemplate: template,
            loanId: "LN-998877",
            borrowerName: "Acme Logistics Corp",
            principalBalance: 100000M,
            customRateDiscount: 0.005M
        );

        // Act
        var interest = loan.GetEffectiveAnnualInterest();
        var disclosure = loan.GetDisclosureStatement();

        // Assert
        // (100,000 * 0.060) - (100,000 * 0.005) = 6,000 - 500 = 5,500
        interest.ShouldBe(5500M);
        disclosure.ShouldContain("Acme Logistics Corp");
        disclosure.ShouldContain("LN-998877");
        disclosure.ShouldContain("Secured equipment lease terms. UCC filing required.");
    }

    [Fact]
    public void MultipleActiveLoans_ShareSameFlyweight_AndComputeIndependentExtrinsicResults()
    {
        // Arrange
        var factory = new LoanProductFlyweightFactory();
        var smeTemplate = factory.GetFlyweight("SME-WORKING-CAPITAL"); // Base rate: 0.085

        var loan1 = new ActiveLoanAccount(smeTemplate, "LN-101", "Borrower A", 50000M, 0.010M);
        var loan2 = new ActiveLoanAccount(smeTemplate, "LN-102", "Borrower B", 200000M, 0.000M);

        // Act
        var interestLoan1 = loan1.GetEffectiveAnnualInterest();
        var interestLoan2 = loan2.GetEffectiveAnnualInterest();

        // Assert
        // loan1: (50,000 * 0.085) - (50,000 * 0.010) = 4,250 - 500 = 3,750
        interestLoan1.ShouldBe(3750M);

        // loan2: (200,000 * 0.085) - (200,000 * 0.000) = 17,000
        interestLoan2.ShouldBe(17000M);
    }

    [Fact]
    public void ActiveLoanAccount_WhenDiscountExceedsBaseRate_FloorsInterestAtZero()
    {
        // Arrange
        var template = new LoanProductTemplateFlyweight("SUB-PROD", "Subsidized lending.", 0.020M);
        var loan = new ActiveLoanAccount(template, "LN-SUB-1", "Civic Org", 50000M, 0.050M);

        // Act
        var interest = loan.GetEffectiveAnnualInterest();

        // Assert
        interest.ShouldBe(0M);
    }
}
