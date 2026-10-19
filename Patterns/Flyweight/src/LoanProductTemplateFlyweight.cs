namespace Flyweight;

public class LoanProductTemplateFlyweight : ILoanProductFlyweight
{
    public string ProductCode { get; }
    private readonly string _regulatoryDisclosureText;
    private readonly decimal _baseRateFactor;

    public LoanProductTemplateFlyweight(string productCode, string regulatoryDisclosureText, decimal baseRateFactor)
    {
        ProductCode = productCode;
        _regulatoryDisclosureText = regulatoryDisclosureText;
        _baseRateFactor = baseRateFactor;
    }

    public string GetRegulatoryDisclosure() => _regulatoryDisclosureText;

    public decimal CalculateBaseInterest(decimal principal) => principal * _baseRateFactor;
}

