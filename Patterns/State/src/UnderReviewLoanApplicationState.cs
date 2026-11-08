namespace State;

internal sealed class UnderReviewLoanApplicationState : LoanApplicationState
{
    public override LoanApplicationStatus Status => LoanApplicationStatus.UnderReview;

    public override void Approve(LoanApplication application) =>
        application.SetState(new ApprovedLoanApplicationState());

    public override void Decline(LoanApplication application, string reason)
    {
        application.SetDeclineReason(reason);
        application.SetState(new DeclinedLoanApplicationState());
    }

    public override void Withdraw(LoanApplication application) =>
        application.SetState(new WithdrawnLoanApplicationState());
}
