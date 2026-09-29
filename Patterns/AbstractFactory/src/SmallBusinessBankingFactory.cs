namespace AbstractFactory;

public class SmallBusinessBankingFactory : ICommercialBankingFactory
{
    public IBankGuarantee CreateBankingGuarantee()
    {
        return new SmallBusinessGuarantee();
    }

    public IBusinessOverdraft CreateBusinessOverdraft()
    {
        return new SmallBusinessOverdraft();
    }
}
