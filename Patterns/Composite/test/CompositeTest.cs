using Composite;
using Shouldly;
using Xunit;

namespace CompositeTest;

public class CompositeShould
{
    [Fact]
    public void ReturnBalancesAndAvailableCredit_WhenComponentIsLoanAccount()
    {
        // Arrange
        var loanAccount = new CreditFacilityLeaf("Term Loan account 1", 400m, 300m, 12m);

        loanAccount.ShouldSatisfyAllConditions(
            "loan account balances",
            () => loanAccount.GetTotalPrincipalBalance().ShouldBe(300m),
            () => loanAccount.GetTotalAccruedInterest().ShouldBe(12m),
            () => loanAccount.GetTotalAvailableCreditLimit().ShouldBe(100m));
    }

    [Fact]
    public void RollUpBalancesAndAvailableCredit_AcrossNestedFacilityComponents()
    {
        // Arrange
        var termLoanTranche = new CreditFacilityComposite("Tranche A - Term Loan");
        termLoanTranche.Add(new CreditFacilityLeaf("Term Loan account 1", 400m, 300m, 12m));
        termLoanTranche.Add(new CreditFacilityLeaf("Term Loan account 2", 100m, 50m, 3m));

        var revolvingTranche = new CreditFacilityLeaf("Tranche B - Revolver", 300m, 120m, 4m);
        var mezzanineTranche = new CreditFacilityLeaf("Tranche C - Mezzanine", 200m, 200m, 10m);

        var masterFacility = new CreditFacilityComposite("Syndicated Credit Facility");
        masterFacility.Add(termLoanTranche);
        masterFacility.Add(revolvingTranche);
        masterFacility.Add(mezzanineTranche);

        // Act
        var principalBalance = masterFacility.GetTotalPrincipalBalance();
        var accruedInterest = masterFacility.GetTotalAccruedInterest();
        var availableCredit = masterFacility.GetTotalAvailableCreditLimit();

        // Assert
        masterFacility.ShouldSatisfyAllConditions(
            "facility totals",
            () => masterFacility.GetTotalPrincipalBalance().ShouldBe(670m),
            () => masterFacility.GetTotalAccruedInterest().ShouldBe(29m),
            () => masterFacility.GetTotalAvailableCreditLimit().ShouldBe(330m));
    }

    [Fact]
    public void ReturnZeroTotals_WhenFacilityHasNoComponents()
    {
        // Arrange
        var facility = new CreditFacilityComposite("Empty Facility");

        // Assert
        facility.ShouldSatisfyAllConditions(
            "facility totals",
            () => facility.GetTotalPrincipalBalance().ShouldBe(0m),
            () => facility.GetTotalAccruedInterest().ShouldBe(0m),
            () => facility.GetTotalAvailableCreditLimit().ShouldBe(0m));
    }
}
