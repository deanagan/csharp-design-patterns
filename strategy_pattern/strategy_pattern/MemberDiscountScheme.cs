using System;


namespace Strategy
{
    public class MemberDiscountScheme : IDiscountScheme
    {
        struct Discount
        {
            public const int BaseMemberDiscount = 5;
        }

        public decimal ComputePrice(IProduct product, ICoupon coupon)
        {
            var memberBaseDiscount = product.SellingPrice() * (Discount.BaseMemberDiscount / 100M);
            var memberDiscountedPrice = product.SellingPrice() - memberBaseDiscount;
            if (coupon.IsExpired())
            {
                return memberDiscountedPrice;
            }
            
            return memberDiscountedPrice - (product.SellingPrice() * (coupon.DiscountPercentage()/ 100M));
        }
       
    }
}