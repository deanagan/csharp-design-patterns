using System;

namespace Strategy;

public sealed class LoanOfferPricer(ILoanPricingStrategy strategy)
{
    private ILoanPricingStrategy _strategy =
        strategy ?? throw new ArgumentNullException(nameof(strategy));

    public void SetStrategy(ILoanPricingStrategy strategy)
    {
        ArgumentNullException.ThrowIfNull(strategy);
        _strategy = strategy;
    }

    public decimal CalculateAnnualInterestRate(LoanPricingRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        return _strategy.CalculateAnnualInterestRate(request);
    }
}
