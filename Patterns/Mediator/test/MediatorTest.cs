using System;
using Mediator;
using NSubstitute;
using Shouldly;
using Xunit;

namespace MediatorTest;

public class MediatorTest
{
    private static LoanApplication CreateApplication(
        bool isSecured = false,
        int creditScore = 720) =>
        new(
            ApplicationId: "LN-1001",
            BorrowerName: "Jordan Lee",
            RequestedAmount: 100_000m,
            AnnualIncome: 120_000m,
            CreditScore: creditScore,
            MonthlyPayment: 2_500m,
            IsSecured: isSecured,
            CollateralValue: isSecured ? 90_000m : null);

    [Fact]
    public void ApprovesApplicationWhenAllChecksPassAndNotifiesBorrower()
    {
        var application = CreateApplication();
        var identityCheck = Substitute.For<IUnderwritingCheck>();
        var creditCheck = Substitute.For<IUnderwritingCheck>();
        var affordabilityCheck = Substitute.For<IUnderwritingCheck>();
        var collateralCheck = Substitute.For<IUnderwritingCheck>();
        var notifier = Substitute.For<ILoanDecisionNotifier>();
        identityCheck.Evaluate(application).Returns(UnderwritingCheckResult.Passed("Identity verified."));
        creditCheck.Evaluate(application).Returns(UnderwritingCheckResult.Passed("Credit passed."));
        affordabilityCheck.Evaluate(application).Returns(UnderwritingCheckResult.Passed("Affordable."));

        var mediator = new LoanUnderwritingMediator(
            [identityCheck, creditCheck, affordabilityCheck],
            collateralCheck,
            notifier);

        var result = mediator.Evaluate(application);

        result.Decision.ShouldBe(LoanDecision.Approved);
        result.Reasons.ShouldBeEmpty();
        collateralCheck.DidNotReceive().Evaluate(Arg.Any<LoanApplication>());
        notifier.Received(1).Notify(application, result);
    }

    [Fact]
    public void RunsCollateralCheckForSecuredApplications()
    {
        var application = CreateApplication(isSecured: true);
        var collateralCheck = Substitute.For<IUnderwritingCheck>();
        collateralCheck.Evaluate(application).Returns(
            UnderwritingCheckResult.Passed("Collateral passed."));
        var mediator = CreateMediator(collateralCheck: collateralCheck);

        var result = mediator.Evaluate(application);

        result.Decision.ShouldBe(LoanDecision.Approved);
        collateralCheck.Received(1).Evaluate(application);
    }

    [Fact]
    public void DeclinesApplicationWhenAnyCheckDeclines()
    {
        var application = CreateApplication();
        var creditCheck = Substitute.For<IUnderwritingCheck>();
        creditCheck.Evaluate(application).Returns(
            UnderwritingCheckResult.Declined("Credit score is too low."));
        var mediator = CreateMediator(standardChecks: [creditCheck]);

        var result = mediator.Evaluate(application);

        result.Decision.ShouldBe(LoanDecision.Declined);
        result.Reasons.ShouldContain("Credit score is too low.");
    }

    [Fact]
    public void RoutesBorderlineApplicationsToManualReview()
    {
        var application = CreateApplication(creditScore: 620);
        var mediator = new LoanUnderwritingMediator(
            [
                new IdentityVerificationCheck(),
                new CreditAssessmentCheck(),
                new AffordabilityCheck()
            ],
            new CollateralCoverageCheck(),
            Substitute.For<ILoanDecisionNotifier>());

        var result = mediator.Evaluate(application);

        result.Decision.ShouldBe(LoanDecision.ManualReview);
        result.Reasons.ShouldContain("Credit score requires a manual underwriting review.");
    }

    [Fact]
    public void DeclinesInsufficientCollateralForSecuredApplication()
    {
        var application = CreateApplication(isSecured: true) with { CollateralValue = 50_000m };
        var mediator = new LoanUnderwritingMediator(
            [new IdentityVerificationCheck(), new CreditAssessmentCheck(), new AffordabilityCheck()],
            new CollateralCoverageCheck(),
            Substitute.For<ILoanDecisionNotifier>());

        var result = mediator.Evaluate(application);

        result.Decision.ShouldBe(LoanDecision.Declined);
        result.Reasons.ShouldContain("Collateral does not meet the minimum 75% coverage requirement.");
    }

    [Fact]
    public void RejectsNullApplication()
    {
        var mediator = CreateMediator();

        Should.Throw<ArgumentNullException>(() => mediator.Evaluate(null!));
    }

    [Fact]
    public void RejectsAnEmptySetOfStandardChecks()
    {
        Should.Throw<ArgumentException>(() =>
            new LoanUnderwritingMediator(
                [],
                Substitute.For<IUnderwritingCheck>(),
                Substitute.For<ILoanDecisionNotifier>()));
    }

    private static LoanUnderwritingMediator CreateMediator(
        IUnderwritingCheck[]? standardChecks = null,
        IUnderwritingCheck? collateralCheck = null)
    {
        var defaultCheck = Substitute.For<IUnderwritingCheck>();
        defaultCheck.Evaluate(Arg.Any<LoanApplication>())
            .Returns(UnderwritingCheckResult.Passed("Check passed."));

        return new LoanUnderwritingMediator(
            standardChecks ?? [defaultCheck],
            collateralCheck ?? Substitute.For<IUnderwritingCheck>(),
            Substitute.For<ILoanDecisionNotifier>());
    }
}
