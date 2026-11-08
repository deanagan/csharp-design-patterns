namespace State;

internal interface ILoanApplicationState
{
    LoanApplicationStatus Status { get; }

    void Submit(LoanApplication application);

    void BeginReview(LoanApplication application);

    void Approve(LoanApplication application);

    void Decline(LoanApplication application, string reason);

    void Fund(LoanApplication application);

    void Withdraw(LoanApplication application);
}
