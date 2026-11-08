namespace State;

internal sealed class SubmittedLoanApplicationState : LoanApplicationState
{
    public override LoanApplicationStatus Status => LoanApplicationStatus.Submitted;

    public override void BeginReview(LoanApplication application) =>
        application.SetState(new UnderReviewLoanApplicationState());

    public override void Withdraw(LoanApplication application) =>
        application.SetState(new WithdrawnLoanApplicationState());
}
