namespace Bridge;

public record PostingResult(
    string ConfirmationCode,
    string LedgerType,
    LedgerTransaction Transaction);

