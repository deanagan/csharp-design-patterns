namespace FactoryMethod;

public interface IBusinessLoanProcessor
{
    decimal CalculateMaxCreditLimit(BusinessProfile profile);
    string GetRepaymentStructureDescription();
}