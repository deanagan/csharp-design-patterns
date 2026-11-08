namespace State;

internal sealed class ApprovedLoanApplicationState : LoanApplicationState
{
    public override LoanApplicationStatus Status => LoanApplicationStatus.Approved;

    public override void Fund(LoanApplication application) =>
        application.SetState(new FundedLoanApplicationState());
}
