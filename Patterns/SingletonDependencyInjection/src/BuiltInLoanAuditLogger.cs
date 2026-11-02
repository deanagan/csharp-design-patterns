using System;

namespace SingletonDI;

public sealed class BuiltInLoanAuditLogger : ILoanAuditLogger
{
    public void LogDecision(string applicationId, string decision)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(applicationId);
        ArgumentException.ThrowIfNullOrWhiteSpace(decision);

        Console.WriteLine($"Loan application {applicationId}: {decision}");
    }
}
