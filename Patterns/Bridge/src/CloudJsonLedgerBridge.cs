using System;

namespace Bridge;

public class CloudJsonLedgerBridge : ILedgerBridge
{
    public PostingResult RecordEntry(LedgerTransaction transaction)
    {
        var uuid = Guid.NewGuid().ToString("N")[..12].ToUpperInvariant();
        var confirmationCode = $"CLOUD-UUID-{uuid}";
        return new PostingResult(confirmationCode, "Cloud-Json", transaction);
    }
}

