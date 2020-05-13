using System;


namespace Strategy
{
    public class NonMemberDiscountScheme : IDiscountScheme
    {
        public decimal ComputePrice(IProduct product, ICoupon coupon)
        {
            if (coupon.IsExpired())
            {
                return product.SellingPrice();
            }
            var discount = product.IsOnSale() ? 0M : (product.SellingPrice() * (coupon.DiscountPercentage()/ 100M));
            return product.SellingPrice() - discount;
        }
    }
}