namespace Decorator;

public class VolumeDiscountDecorator(ILoan loan, decimal currentPrincipal, decimal threshold, decimal rateDiscount) : LoanDecorator(loan)
{
    private readonly decimal _principalThreshold = threshold;
    private readonly decimal _rateDiscount = rateDiscount;
    private readonly decimal _currentPrincipal = currentPrincipal;

    public override decimal GetInterestRate()
    {
        var rate = base.GetInterestRate();
        return _currentPrincipal >= _principalThreshold ? rate - _rateDiscount : rate;
    }

    public override string GetDescription()
    {
        var desc = base.GetDescription();
        return _currentPrincipal >= _principalThreshold ? $"{desc} + Large Volume Discount (-{_rateDiscount * 100}%)" : desc;
    }
}