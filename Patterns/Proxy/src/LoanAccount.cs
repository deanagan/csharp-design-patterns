namespace Proxy;

public sealed record LoanAccount(
    string LoanId,
    string BorrowerId,
    decimal OutstandingBalance,
    string RepaymentInstructions);
