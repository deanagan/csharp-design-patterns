namespace Mediator;

public sealed class IdentityVerificationCheck : IUnderwritingCheck
{
    public UnderwritingCheckResult Evaluate(LoanApplication application)
    {
        if (string.IsNullOrWhiteSpace(application.ApplicationId)
            || string.IsNullOrWhiteSpace(application.BorrowerName))
        {
            return UnderwritingCheckResult.Declined(
                "Application ID and borrower name are required for identity verification.");
        }

        return UnderwritingCheckResult.Passed("Borrower identity details are present.");
    }
}
