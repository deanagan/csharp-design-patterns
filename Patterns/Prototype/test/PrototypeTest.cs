using System;
using Prototype;
using Shouldly;
using Xunit;

namespace PrototypeTest;

public class PrototypeTest
{
    [Fact]
    public void ShallowClonePreservesProductConfigurationAndSharesNestedTerms()
    {
        var prototype = CreatePrototype();

        var offer = (LoanProductPrototype)prototype.Clone();
        offer.ProductCode.ShouldBe(prototype.ProductCode);
        offer.ProductName.ShouldBe(prototype.ProductName);
        offer.Terms.ShouldBeSameAs(prototype.Terms);
    }

    [Fact]
    public void DeepCloneCreatesIndependentLoanTermsForBorrowerOffer()
    {
        var prototype = CreatePrototype();

        var offer = prototype.DeepClone();
        offer.Terms.Revise(75_000m, 0.079m, 48, 750m);

        offer.Terms.ShouldNotBeSameAs(prototype.Terms);
        offer.Terms.RequestedAmount.ShouldBe(75_000m);
        offer.Terms.AnnualInterestRate.ShouldBe(0.079m);
        offer.Terms.TermInMonths.ShouldBe(48);
        offer.Terms.OriginationFee.ShouldBe(750m);

        prototype.Terms.RequestedAmount.ShouldBe(100_000m);
        prototype.Terms.AnnualInterestRate.ShouldBe(0.065m);
        prototype.Terms.TermInMonths.ShouldBe(60);
        prototype.Terms.OriginationFee.ShouldBe(1_000m);
    }

    [Fact]
    public void ShallowCloneSharesChangesToNestedTerms()
    {
        var prototype = CreatePrototype();

        var offer = (LoanProductPrototype)prototype.Clone();
        offer.Terms.Revise(75_000m, 0.079m, 48, 750m);

        prototype.Terms.RequestedAmount.ShouldBe(75_000m);
    }

    [Fact]
    public void RejectsInvalidLoanTerms()
    {
        Should.Throw<ArgumentOutOfRangeException>(() =>
            new LoanProductTerms(0m, 0.065m, 60, 1_000m));
    }

    private static LoanProductPrototype CreatePrototype() =>
        new(
            "SME-WORKING-CAPITAL",
            "SME Working Capital Loan",
            new LoanProductTerms(
                requestedAmount: 100_000m,
                annualInterestRate: 0.065m,
                termInMonths: 60,
                originationFee: 1_000m));
}
