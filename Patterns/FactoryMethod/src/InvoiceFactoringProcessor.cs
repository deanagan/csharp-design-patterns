namespace FactoryMethod;

public class InvoiceFactoringProcessor : IBusinessLoanProcessor
{
    public decimal CalculateMaxCreditLimit(BusinessProfile profile)
    {
        // Based on eligible unpaid B2B invoices (e.g., 80% advance rate)
        return profile.TotalOutstandingInvoices * 0.80M;
    }

    public string GetRepaymentStructureDescription() => "Direct collection from customer invoice settlements.";
}