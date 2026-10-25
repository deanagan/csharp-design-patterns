namespace Mediator;

public sealed class CreditAssessmentCheck : IUnderwritingCheck
{
    public const int DeclineThreshold = 580;
    public const int ManualReviewThreshold = 660;

    public UnderwritingCheckResult Evaluate(LoanApplication application)
    {
        if (application.CreditScore < DeclineThreshold)
        {
            return UnderwritingCheckResult.Declined("Credit score is below the lending minimum.");
        }

        if (application.CreditScore < ManualReviewThreshold)
        {
            return UnderwritingCheckResult.RequiresManualReview(
                "Credit score requires a manual underwriting review.");
        }

        return UnderwritingCheckResult.Passed("Credit score meets the lending threshold.");
    }
}
