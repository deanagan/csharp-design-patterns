using System;


namespace Strategy
{
    public class NonMember : ICustomer
    {
        private readonly ICoupon _coupon;
        public NonMember(ICoupon coupon)
        {
            _coupon = coupon;
        }
        public bool IsMember()
        {
            return false;
        }
        public double CalculatePrice(IProduct product)
        {
            return 0.0;
        }
    }
}