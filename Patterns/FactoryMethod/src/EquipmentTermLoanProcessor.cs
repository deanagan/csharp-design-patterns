namespace FactoryMethod;

public class EquipmentTermLoanProcessor : IBusinessLoanProcessor
{
    public decimal CalculateMaxCreditLimit(BusinessProfile profile)
    {
        // Based on appraised equipment liquidation value
        return profile.AppraisedEquipmentValue * 0.90M;
    }

    public string GetRepaymentStructureDescription() => "Fixed monthly principal and interest amortization.";
}