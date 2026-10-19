namespace Flyweight;

public interface ILoanProductFlyweight
{
    string ProductCode { get; }
    string GetRegulatoryDisclosure();
    decimal CalculateBaseInterest(decimal principal);
}

