using System;

namespace Strategy;

public sealed class RiskBasedLoanPricingStrategy : ILoanPricingStrategy
{
    public decimal CalculateAnnualInterestRate(LoanPricingRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return request.CreditScore switch
        {
            >= 780 => 0.06m,
            >= 720 => 0.075m,
            >= 660 => 0.095m,
            >= 580 => 0.12m,
            _ => throw new InvalidOperationException(
                "Credit score is below the minimum for this loan pricing strategy.")
        };
    }
}
