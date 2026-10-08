namespace Builder;

public class LoanUnderwritingDirector
{
    public LoanApplication ConstructStandardPersonalLoan(
        ILoanApplicationBuilder builder,
        string applicationId,
        string borrowerName,
        int creditScore,
        decimal annualIncome,
        decimal amount)
    {
        return builder
            .SetApplicationId(applicationId)
            .SetBorrower(borrowerName, creditScore, annualIncome)
            .SetLoanTerms(amount, termInMonths: 36, interestRate: 0.089m)
            .SetRepaymentFrequency(RepaymentFrequency.Monthly)
            .SetOriginationFee(amount * 0.015m)
            .Build();
    }

    public LoanApplication ConstructSecuredCommercialFacility(
        ILoanApplicationBuilder builder,
        string applicationId,
        string businessOwnerName,
        string businessRegNumber,
        decimal annualRevenue,
        decimal requestedAmount,
        string collateralAsset,
        decimal collateralValue)
    {
        return builder
            .SetApplicationId(applicationId)
            .SetBorrower(businessOwnerName, creditScore: 720, annualIncome: annualRevenue)
            .SetBusinessRegistration(businessRegNumber)
            .SetLoanTerms(requestedAmount, termInMonths: 60, interestRate: 0.065m)
            .SetCollateral(collateralAsset, collateralValue)
            .SetRepaymentFrequency(RepaymentFrequency.Monthly)
            .SetOriginationFee(requestedAmount * 0.01m)
            .Build();
    }
}

