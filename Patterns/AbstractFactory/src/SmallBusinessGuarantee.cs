namespace AbstractFactory;

public class SmallBusinessGuarantee : IBankGuarantee
{
    public decimal GetGuaranteeAmount()
    {
        return 50000m;
    }

    public string GetDescription()
    {
        return "Small Business banking guarantee";
    }

    public string GetBeneficiary()
    {
        return "Small Business Beneficiary";
    }
}
