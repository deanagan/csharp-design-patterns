using System;


namespace Strategy
{
    public class Member : ICustomer
    {
        private readonly ICoupon _coupon;
        
        struct Discount
        {
            public const int BaseMemberDiscount = 5;
        }

        public Member(ICoupon coupon)
        {
            _coupon = coupon;
        }
        public bool IsMember()
        {
            return true;
        }
        public Decimal CalculatePrice(IProduct product)
        {            
            var memberBaseDiscount = product.SellingPrice() * (Discount.BaseMemberDiscount / 100M);
            var memberDiscountedPrice = product.SellingPrice() - memberBaseDiscount;
            if (_coupon.IsExpired())
            {
                return memberDiscountedPrice;
            }
            
            return memberDiscountedPrice - (product.SellingPrice() * (_coupon.DiscountPercentage()/ 100M));
        }
    }
}