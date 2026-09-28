namespace AbstractFactory;

public class CorporateBankGuarantee : IBankGuarantee
{
    public decimal GetGuaranteeAmount()
    {
        return 1000000m;
    }

    public string GetDescription()
    {
        return "Corporate Bank Guarantee";
    }

    public string GetBeneficiary()
    {
        return "Corporate Client";
    }
}