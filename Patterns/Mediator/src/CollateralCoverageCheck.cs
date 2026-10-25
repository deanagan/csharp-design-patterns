namespace Mediator;

public sealed class CollateralCoverageCheck : IUnderwritingCheck
{
    public const decimal MinimumCoverageRatio = 0.75m;

    public UnderwritingCheckResult Evaluate(LoanApplication application)
    {
        if (application.CollateralValue is not decimal collateralValue
            || collateralValue < application.RequestedAmount * MinimumCoverageRatio)
        {
            return UnderwritingCheckResult.Declined(
                "Collateral does not meet the minimum 75% coverage requirement.");
        }

        return UnderwritingCheckResult.Passed("Collateral meets the minimum coverage requirement.");
    }
}
