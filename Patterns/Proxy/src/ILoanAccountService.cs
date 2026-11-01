namespace Proxy;

public interface ILoanAccountService
{
    LoanAccount? GetAccount(string loanId);

    bool UpdateRepaymentInstructions(string loanId, string repaymentInstructions);

    bool InitiatePayout(string loanId, decimal amount);
}
