namespace Decorator;

public class CollateralDiscountDecorator(ILoan loan, bool hasCollateral, decimal rateDiscount) : LoanDecorator(loan)
{
    private readonly bool _hasCollateral = hasCollateral;
    private readonly decimal _rateDiscount = rateDiscount;

    public override decimal GetInterestRate()
    {
        var rate = base.GetInterestRate();
        return _hasCollateral ? rate - _rateDiscount : rate;
    }

    public override string GetDescription()
    {
        var desc = base.GetDescription();
        return _hasCollateral ? $"{desc} + Secured Property Discount (-{_rateDiscount * 100}%)" : desc;
    }
}
