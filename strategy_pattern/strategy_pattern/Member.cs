using System;


namespace Strategy
{
    public class Member : ICustomer
    {
        private readonly ICoupon _coupon;
        
        struct Discount
        {
            public const double BaseMemberDiscount = 0.05;
        }

        public Member(ICoupon coupon)
        {
            _coupon = coupon;
        }
        public bool IsMember()
        {
            return true;
        }
        public double CalculatePrice(IProduct product)
        {            
            var memberBaseDiscount = product.SellingPrice() * Discount.BaseMemberDiscount;
            var memberDiscountedPrice = product.SellingPrice() - memberBaseDiscount;
            if (_coupon.IsExpired())
            {
                return memberDiscountedPrice;
            }
            
            return memberDiscountedPrice - (product.SellingPrice() * _coupon.Discount());
        }
    }
}