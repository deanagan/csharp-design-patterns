namespace State;

internal sealed class DraftLoanApplicationState : LoanApplicationState
{
    public override LoanApplicationStatus Status => LoanApplicationStatus.Draft;

    public override void Submit(LoanApplication application) =>
        application.SetState(new SubmittedLoanApplicationState());
}
