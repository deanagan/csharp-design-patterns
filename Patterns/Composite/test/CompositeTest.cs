using FluentAssertions;
using Xunit;

namespace Composite.Test
{
    public class CompositeShould
    {
        [Fact]
        public void ReturnBalancesAndAvailableCredit_WhenComponentIsDrawdownLoan()
        {
            // Arrange
            var drawdown = new CreditFacilityLeaf("Term Loan drawdown 1", 400m, 300m, 12m);

            drawdown.GetTotalPrincipalBalance().Should().Be(300m);
            drawdown.GetTotalAccruedInterest().Should().Be(12m);
            drawdown.GetTotalAvailableCreditLimit().Should().Be(100m);
        }

        [Fact]
        public void RollUpBalancesAndAvailableCredit_AcrossNestedFacilityComponents()
        {
            // Arrange
            var termLoanTranche = new CreditFacilityComposite("Tranche A - Term Loan");
            termLoanTranche.Add(new CreditFacilityLeaf("Term Loan drawdown 1", 400m, 300m, 12m));
            termLoanTranche.Add(new CreditFacilityLeaf("Term Loan drawdown 2", 100m, 50m, 3m));

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
            principalBalance.Should().Be(670m);
            accruedInterest.Should().Be(29m);
            availableCredit.Should().Be(330m);
        }

        [Fact]
        public void ReturnZeroTotals_WhenFacilityHasNoComponents()
        {
            // Arrange
            var facility = new CreditFacilityComposite("Empty Facility");

            // Assert
            facility.GetTotalPrincipalBalance().Should().Be(0m);
            facility.GetTotalAccruedInterest().Should().Be(0m);
            facility.GetTotalAvailableCreditLimit().Should().Be(0m);
        }
    }
}
