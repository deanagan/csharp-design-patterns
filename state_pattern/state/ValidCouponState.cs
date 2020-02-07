using System;

namespace State
{
    public class ValidCouponState : ICouponState
    {
        public void ChangeExpiryDate(DateTime dateTime, ICoupon coupon)
        {

        }

        public decimal GetDiscount()
        {
            return 0.0M;
        }
    }
    
}