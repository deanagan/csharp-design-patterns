using Decorator;
using NSubstitute;
using Shouldly;
using Xunit;

namespace DecoratorTest;

public class DecoratorShould
{
    private const decimal BaseInterestRate = 0.08m;
    private const decimal CollateralDiscount = 0.01m;
    private const decimal TierDiscount = 0.005m;
    private const decimal VolumeDiscount = 0.0025m;
    private const decimal VolumeThreshold = 250_000m;

    [Fact]
    public void ReturnBaseTerms_ForStandardLoan()
    {
        var loan = new BaseLoan(120_000m, BaseInterestRate);

        loan.GetInterestRate().ShouldBe(BaseInterestRate);
        loan.GetMonthlyPayment().ShouldBeInRange(799.99m, 800.01m);
        loan.GetDescription().ShouldBe("Standard Base Loan");
    }

    [Fact]
    public void ApplyCollateralDiscount_ForSecuredLoan()
    {
        var loan = BuildLoan(hasCollateral: true);

        loan.GetInterestRate().ShouldBe(BaseInterestRate - CollateralDiscount);
        loan.GetMonthlyPayment().ShouldBeInRange(799.99m, 800.01m);
        loan.GetDescription().ShouldContain("Secured Property Discount");
    }

    [Fact]
    public void ApplyTierDiscount_ForTierOneCustomer()
    {
        var loan = BuildLoan(customerTier: 1);

        loan.GetInterestRate().ShouldBe(BaseInterestRate - TierDiscount);
        loan.GetDescription().ShouldContain("Tier 1 VIP Discount");
    }

    [Fact]
    public void ApplyVolumeDiscount_WhenPrincipalMeetsThreshold()
    {
        var loan = BuildLoan(principal: VolumeThreshold);

        loan.GetInterestRate().ShouldBe(BaseInterestRate - VolumeDiscount);
        loan.GetDescription().ShouldContain("Large Volume Discount");
    }

    [Fact]
    public void StackEligibleDiscounts_WhenLoanIsSecuredTierOneAndHighVolume()
    {
        var loan = BuildLoan(
            principal: VolumeThreshold,
            hasCollateral: true,
            customerTier: 1);

        loan.GetInterestRate().ShouldBe(
            BaseInterestRate - CollateralDiscount - TierDiscount - VolumeDiscount);
        loan.GetMonthlyPayment().ShouldBeInRange(1_666.66m, 1_666.68m);
        loan.GetDescription().ShouldContain("Secured Property Discount");
        loan.GetDescription().ShouldContain("Tier 1 VIP Discount");
        loan.GetDescription().ShouldContain("Large Volume Discount");
    }

    private static ILoan BuildLoan(
        decimal principal = 120_000m,
        bool hasCollateral = false,
        int customerTier = 0)
    {
        ILoan loan = Substitute.For<ILoan>();
        loan.GetInterestRate().Returns(BaseInterestRate);
        loan.GetMonthlyPayment().Returns(principal * (BaseInterestRate / 12m));
        loan.GetDescription().Returns("Standard Base Loan");

        if (hasCollateral)
        {
            loan = new CollateralDiscountDecorator(loan, true, CollateralDiscount);
        }

        loan = new TierDiscountDecorator(loan, customerTier, TierDiscount);
        loan = new VolumeDiscountDecorator(loan, principal, VolumeThreshold, VolumeDiscount);

        return loan;
    }
}
