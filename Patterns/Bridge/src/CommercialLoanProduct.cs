namespace Bridge;

public class CommercialLoanProduct(ILedgerBridge ledgerBridge) : LoanProduct(ledgerBridge)
{
    public const string ReceivableAccount = "1052-COMMERCIAL-LOANS";
    public const string SettlementAccount = "1010-CASH-SETTLEMENT";

    public override PostingResult Disburse(string loanId, decimal amount)
    {
        var transaction = new LedgerTransaction(
            ReferenceId: loanId,
            Amount: amount,
            DebitAccount: ReceivableAccount,
            CreditAccount: SettlementAccount);

        return LedgerBridge.RecordEntry(transaction);
    }

    public override PostingResult ApplyRepayment(string loanId, decimal amount)
    {
        var transaction = new LedgerTransaction(
            ReferenceId: loanId,
            Amount: amount,
            DebitAccount: SettlementAccount,
            CreditAccount: ReceivableAccount);

        return LedgerBridge.RecordEntry(transaction);
    }
}

