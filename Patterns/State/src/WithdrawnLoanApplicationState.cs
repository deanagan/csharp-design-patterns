namespace State;

internal sealed class WithdrawnLoanApplicationState : LoanApplicationState
{
    public override LoanApplicationStatus Status => LoanApplicationStatus.Withdrawn;
}
