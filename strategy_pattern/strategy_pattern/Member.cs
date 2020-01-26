using System;


namespace Strategy
{
    public class Member : ICustomer
    {
        private readonly ICoupon _coupon;

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
            return product.SellingPrice() - (product.SellingPrice()*_coupon.Discount());
        }
    }
}