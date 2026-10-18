namespace Decorator;

public class BaseLoan(decimal principal, decimal baseInterestRate) : ILoan
{
    private readonly decimal _principal = principal;
    private readonly decimal _baseInterestRate = baseInterestRate;

    public decimal GetInterestRate() => _baseInterestRate;
    public decimal GetMonthlyPayment() => _principal * (_baseInterestRate / 12);
    public string GetDescription() => "Standard Base Loan";
}
