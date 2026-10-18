namespace Decorator;

public abstract class LoanDecorator(ILoan loan) : ILoan
{
    public virtual decimal GetInterestRate() => loan.GetInterestRate();
    public virtual decimal GetMonthlyPayment() => loan.GetMonthlyPayment();
    public virtual string GetDescription() => loan.GetDescription();
}
