namespace Decorator;

public class TierDiscountDecorator(ILoan loan, int customerTier, decimal rateDiscount) : LoanDecorator(loan)
{
    public override decimal GetInterestRate()
    {
        var rate = base.GetInterestRate();
        return customerTier >= 1 ? rate - rateDiscount : rate;
    }

    public override string GetDescription()
    {
        var desc = base.GetDescription();
        return customerTier >= 1 ? $"{desc} + Tier 1 VIP Discount (-{rateDiscount * 100}%)" : desc;
    }
}