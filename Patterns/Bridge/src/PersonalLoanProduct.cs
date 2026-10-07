namespace Bridge;

public class PersonalLoanProduct(ILedgerBridge ledgerBridge) : LoanProduct(ledgerBridge)
{
    public const string ReceivableAccount = "1051-PERSONAL-LOANS";
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

