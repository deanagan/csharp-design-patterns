
namespace AbstractFactory;

    // Using Generic Constraints are an option, but not necessary for this example.
public interface ICommercialBankingFactory
{
    IBankGuarantee CreateBankingGuarantee();
    IBusinessOverdraft CreateBusinessOverdraft();
}
