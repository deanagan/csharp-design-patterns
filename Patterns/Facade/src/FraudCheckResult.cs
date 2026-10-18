namespace Facade;

public record FraudCheckResult(
    bool IsPassed,
    int RiskScore,
    string? Reason = null);

