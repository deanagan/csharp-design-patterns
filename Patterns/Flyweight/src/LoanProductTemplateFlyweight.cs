namespace Flyweight;

public class LoanProductTemplateFlyweight(
    string productCode,
    string regulatoryDisclosureText,
    decimal baseRateFactor) : ILoanProductFlyweight
{
    public string ProductCode { get; } = productCode;
    private readonly string _regulatoryDisclosureText = regulatoryDisclosureText;
    private readonly decimal _baseRateFactor = baseRateFactor;

    public string GetRegulatoryDisclosure() => _regulatoryDisclosureText;

    public decimal CalculateBaseInterest(decimal principal) => principal * _baseRateFactor;
}

