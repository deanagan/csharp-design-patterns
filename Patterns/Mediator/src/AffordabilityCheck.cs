namespace Mediator;

public sealed class AffordabilityCheck : IUnderwritingCheck
{
    public const decimal MaximumAnnualRepaymentToIncomeRatio = 0.40m;

    public UnderwritingCheckResult Evaluate(LoanApplication application)
    {
        if (application.AnnualIncome <= 0m)
        {
            return UnderwritingCheckResult.Declined("Annual income must be greater than zero.");
        }

        var annualRepayments = application.MonthlyPayment * 12m;
        if (annualRepayments / application.AnnualIncome > MaximumAnnualRepaymentToIncomeRatio)
        {
            return UnderwritingCheckResult.Declined(
                "Proposed repayments exceed the maximum affordability threshold.");
        }

        return UnderwritingCheckResult.Passed("Proposed repayments meet the affordability threshold.");
    }
}
