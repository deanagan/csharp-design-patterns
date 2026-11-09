using System;
using Shouldly;
using Xunit;

namespace Strategy.Test;

public class StrategyTest
{
    [Theory]
    [InlineData(800, 0.06)]
    [InlineData(750, 0.075)]
    [InlineData(680, 0.095)]
    [InlineData(600, 0.12)]
    public void CalculatesRiskBasedRateForCreditTier(int creditScore, decimal expectedRate)
    {
        var pricer = new LoanOfferPricer(new RiskBasedLoanPricingStrategy());

        var rate = pricer.CalculateAnnualInterestRate(
            new LoanPricingRequest(100_000m, creditScore));

        rate.ShouldBe(expectedRate);
    }

    [Fact]
    public void CalculatesSecuredRateWhenCollateralMeetsCoverageRequirement()
    {
        var pricer = new LoanOfferPricer(new SecuredLoanPricingStrategy());

        var rate = pricer.CalculateAnnualInterestRate(
            new LoanPricingRequest(
                requestedAmount: 100_000m,
                creditScore: 700,
                isSecured: true,
                collateralValue: 75_000m));

        rate.ShouldBe(0.065m);
    }

    [Fact]
    public void RejectsSecuredPricingForAnUnsecuredApplication()
    {
        var strategy = new SecuredLoanPricingStrategy();

        Should.Throw<InvalidOperationException>(() =>
            strategy.CalculateAnnualInterestRate(new LoanPricingRequest(100_000m, 700)));
    }

    [Fact]
    public void RejectsSecuredPricingWhenCollateralCoverageIsInsufficient()
    {
        var strategy = new SecuredLoanPricingStrategy();
        var request = new LoanPricingRequest(
            requestedAmount: 100_000m,
            creditScore: 700,
            isSecured: true,
            collateralValue: 74_999m);

        Should.Throw<InvalidOperationException>(() => strategy.CalculateAnnualInterestRate(request));
    }

    [Fact]
    public void RejectsCreditScoreBelowTheSelectedStrategyMinimum()
    {
        var strategy = new RiskBasedLoanPricingStrategy();

        Should.Throw<InvalidOperationException>(() =>
            strategy.CalculateAnnualInterestRate(new LoanPricingRequest(100_000m, 550)));
    }

    [Fact]
    public void AllowsPricingStrategyToBeChangedForTheSameOfferContext()
    {
        var pricer = new LoanOfferPricer(new RiskBasedLoanPricingStrategy());
        var request = new LoanPricingRequest(
            requestedAmount: 100_000m,
            creditScore: 700,
            isSecured: true,
            collateralValue: 80_000m);

        var riskBasedRate = pricer.CalculateAnnualInterestRate(request);
        pricer.SetStrategy(new SecuredLoanPricingStrategy());
        var securedRate = pricer.CalculateAnnualInterestRate(request);

        riskBasedRate.ShouldBe(0.095m);
        securedRate.ShouldBe(0.065m);
    }

    [Fact]
    public void RejectsNullStrategyAndRequest()
    {
        var pricer = new LoanOfferPricer(new RiskBasedLoanPricingStrategy());

        Should.Throw<ArgumentNullException>(() => pricer.SetStrategy(null!));
        Should.Throw<ArgumentNullException>(() => pricer.CalculateAnnualInterestRate(null!));
        Should.Throw<ArgumentNullException>(() => new LoanOfferPricer(null!));
    }
}
