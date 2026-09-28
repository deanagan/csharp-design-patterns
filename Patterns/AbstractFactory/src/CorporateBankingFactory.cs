namespace AbstractFactory;

public class CorporateBankingFactory : ICommercialBankingFactory
{
    public IBankGuarantee CreateBankingGuarantee()
    {
        return new CorporateBankGuarantee();
    }

    public IBusinessOverdraft CreateBusinessOverdraft()
    {
        return new CorporateBusinessOverdraft();
    }
}