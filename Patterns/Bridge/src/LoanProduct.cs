namespace Bridge;

public abstract class LoanProduct(ILedgerBridge ledgerBridge)
{
    protected ILedgerBridge LedgerBridge { get; } = ledgerBridge;

    public abstract PostingResult Disburse(string loanId, decimal amount);
    public abstract PostingResult ApplyRepayment(string loanId, decimal amount);
}

