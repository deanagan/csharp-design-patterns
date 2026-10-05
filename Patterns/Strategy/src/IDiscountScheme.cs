namespace Strategy
{
    public interface IDiscountScheme
    {
        decimal ComputePrice(IProduct product, ICoupon coupon);
    }
}