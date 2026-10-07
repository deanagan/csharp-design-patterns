namespace Bridge;

public interface ILedgerBridge
{
    PostingResult RecordEntry(LedgerTransaction transaction);
}

