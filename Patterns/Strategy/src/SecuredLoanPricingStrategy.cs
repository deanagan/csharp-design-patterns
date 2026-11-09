using System;

namespace Strategy;

public sealed class SecuredLoanPricingStrategy : ILoanPricingStrategy
{
    public const decimal MinimumCollateralCoverageRatio = 0.75m;

    public decimal CalculateAnnualInterestRate(LoanPricingRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (!request.IsSecured)
        {
            throw new InvalidOperationException(
                "Secured loan pricing requires a secured loan application.");
        }

        var requiredCollateral = request.RequestedAmount * MinimumCollateralCoverageRatio;
        if (request.CollateralValue is not decimal collateralValue
            || collateralValue < requiredCollateral)
        {
            throw new InvalidOperationException(
                $"Collateral must cover at least {MinimumCollateralCoverageRatio:P0} of the requested amount.");
        }

        return request.CreditScore switch
        {
            >= 660 => 0.065m,
            >= 580 => 0.085m,
            _ => throw new InvalidOperationException(
                "Credit score is below the minimum for this secured loan pricing strategy.")
        };
    }
}
