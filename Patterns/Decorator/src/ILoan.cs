namespace Decorator;

public interface ILoan
{
    decimal GetInterestRate();
    decimal GetMonthlyPayment();
    string GetDescription();
}
