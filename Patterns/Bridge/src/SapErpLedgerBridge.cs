using System;

namespace Bridge;

public class SapErpLedgerBridge : ILedgerBridge
{
    public PostingResult RecordEntry(LedgerTransaction transaction)
    {
        var hash = Math.Abs(transaction.ReferenceId.GetHashCode());
        var confirmationCode = $"SAP-DOC-{hash:X8}";
        return new PostingResult(confirmationCode, "SAP-ERP", transaction);
    }
}

