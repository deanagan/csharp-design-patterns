using System;

namespace Strategy;

public sealed record LoanPricingRequest
{
    public LoanPricingRequest(
        decimal requestedAmount,
        int creditScore,
        bool isSecured = false,
        decimal? collateralValue = null)
    {
        if (requestedAmount <= 0m)
        {
            throw new ArgumentOutOfRangeException(nameof(requestedAmount));
        }

        if (creditScore is < 300 or > 850)
        {
            throw new ArgumentOutOfRangeException(nameof(creditScore));
        }

        if (collateralValue is < 0m)
        {
            throw new ArgumentOutOfRangeException(nameof(collateralValue));
        }

        RequestedAmount = requestedAmount;
        CreditScore = creditScore;
        IsSecured = isSecured;
        CollateralValue = collateralValue;
    }

    public decimal RequestedAmount { get; }

    public int CreditScore { get; }

    public bool IsSecured { get; }

    public decimal? CollateralValue { get; }
}
