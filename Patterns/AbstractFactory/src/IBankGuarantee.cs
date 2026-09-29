namespace AbstractFactory;

public interface IBankGuarantee
{
    decimal GetGuaranteeAmount();
    string GetDescription();
    string GetBeneficiary();
}
