namespace Builder;

public interface ILoanApplicationBuilder
{
    ILoanApplicationBuilder SetApplicationId(string applicationId);
    ILoanApplicationBuilder SetBorrower(string fullName, int creditScore, decimal annualIncome);
    ILoanApplicationBuilder SetLoanTerms(decimal principalAmount, int termInMonths, decimal interestRate);
    ILoanApplicationBuilder SetRepaymentFrequency(RepaymentFrequency frequency);
    ILoanApplicationBuilder SetOriginationFee(decimal feeAmount);
    ILoanApplicationBuilder SetCollateral(string assetType, decimal estimatedValue);
    ILoanApplicationBuilder SetBusinessRegistration(string registrationNumber);
    ILoanApplicationBuilder SetGuarantorRequired(bool isRequired);
    LoanApplication Build();
}

