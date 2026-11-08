namespace State;

internal sealed class DeclinedLoanApplicationState : LoanApplicationState
{
    public override LoanApplicationStatus Status => LoanApplicationStatus.Declined;
}
