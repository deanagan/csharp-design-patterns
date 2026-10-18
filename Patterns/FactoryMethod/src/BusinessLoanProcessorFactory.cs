using System;

namespace FactoryMethod;

public static class BusinessLoanProcessorFactory
{
    public static IBusinessLoanProcessor Create(BusinessLoanType loanType)
    {
        return loanType switch
        {
            BusinessLoanType.MerchantCashAdvance => new MerchantCashAdvanceProcessor(),
            BusinessLoanType.InvoiceFactoring => new InvoiceFactoringProcessor(),
            BusinessLoanType.EquipmentTermLoan => new EquipmentTermLoanProcessor(),
            _ => throw new NotSupportedException($"Unsupported business loan type: {loanType}")
        };
    }
}