namespace SingletonDI;

public interface ILoanAuditLogger
{
    void LogDecision(string applicationId, string decision);
}
