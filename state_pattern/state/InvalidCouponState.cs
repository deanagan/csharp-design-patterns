using System;

namespace State
{
    public class InvalidCouponState : ICouponState
    {
        public void ChangeExpiryDate(DateTime dateTime, ICoupon coupon)
        {

        }

        public bool UseDiscount()
        {
            return false;
        }
    }
    
}