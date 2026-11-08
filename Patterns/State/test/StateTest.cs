using System;
using Shouldly;
using Xunit;

namespace State.Test;

public class StateTest
{
    [Fact]
    public void TransitionsFromDraftToFundedThroughTheLoanLifecycle()
    {
        var application = CreateApplication();

        application.Status.ShouldBe(LoanApplicationStatus.Draft);
        application.Submit();
        application.Status.ShouldBe(LoanApplicationStatus.Submitted);
        application.BeginReview();
        application.Status.ShouldBe(LoanApplicationStatus.UnderReview);
        application.Approve();
        application.Status.ShouldBe(LoanApplicationStatus.Approved);
        application.Fund();
        application.Status.ShouldBe(LoanApplicationStatus.Funded);
    }

    [Fact]
    public void AllowsDecliningAnApplicationUnderReviewWithAReason()
    {
        var application = CreateApplication();
        application.Submit();
        application.BeginReview();

        application.Decline("Debt-to-income ratio exceeds policy.");

        application.Status.ShouldBe(LoanApplicationStatus.Declined);
        application.DeclineReason.ShouldBe("Debt-to-income ratio exceeds policy.");
    }

    [Fact]
    public void AllowsWithdrawalBeforeFunding()
    {
        var application = CreateApplication();
        application.Submit();

        application.Withdraw();

        application.Status.ShouldBe(LoanApplicationStatus.Withdrawn);
    }

    [Fact]
    public void RejectsActionsThatAreInvalidForTheCurrentState()
    {
        var application = CreateApplication();

        Should.Throw<InvalidOperationException>(() => application.BeginReview());
        Should.Throw<InvalidOperationException>(() => application.Approve());
        Should.Throw<InvalidOperationException>(() => application.Fund());
        Should.Throw<InvalidOperationException>(() => application.Withdraw());
    }

    [Fact]
    public void DoesNotAllowFurtherActionsAfterFunding()
    {
        var application = CreateApplication();
        application.Submit();
        application.BeginReview();
        application.Approve();
        application.Fund();

        Should.Throw<InvalidOperationException>(() => application.Withdraw());
        Should.Throw<InvalidOperationException>(() => application.Decline("Changed decision."));
    }

    [Fact]
    public void RequiresDeclineReason()
    {
        var application = CreateApplication();
        application.Submit();
        application.BeginReview();

        Should.Throw<ArgumentException>(() => application.Decline(" "));
        application.Status.ShouldBe(LoanApplicationStatus.UnderReview);
    }

    private static LoanApplication CreateApplication() =>
        new("LN-1001", "Jordan Lee", 100_000m);
}
