namespace FactoryMethod;

public class MerchantCashAdvanceProcessor : IBusinessLoanProcessor
{
    public decimal CalculateMaxCreditLimit(BusinessProfile profile)
    {
        // Based on average monthly credit card processing volume
        return profile.AverageMonthlyCardSales * 1.5M; 
    }

    public string GetRepaymentStructureDescription() => "Variable daily remittance via payment gateway split.";
}