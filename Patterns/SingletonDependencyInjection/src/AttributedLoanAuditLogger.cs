using System;

namespace SingletonDI;

[Singleton]
public sealed class AttributedLoanAuditLogger : ILoanAuditLogger
{
    public void LogDecision(string applicationId, string decision)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(applicationId);
        ArgumentException.ThrowIfNullOrWhiteSpace(decision);

        Console.WriteLine($"Loan application {applicationId}: {decision}");
    }
}
