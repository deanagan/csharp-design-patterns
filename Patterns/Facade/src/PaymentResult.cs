namespace Facade;

public record PaymentResult(
    bool IsSuccessful,
    string? TransactionId,
    string? FailureReason,
    string? LedgerReference);

