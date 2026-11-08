namespace State;

internal sealed class FundedLoanApplicationState : LoanApplicationState
{
    public override LoanApplicationStatus Status => LoanApplicationStatus.Funded;
}
