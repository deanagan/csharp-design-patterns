namespace Strategy;

public interface ILoanPricingStrategy
{
    decimal CalculateAnnualInterestRate(LoanPricingRequest request);
}
