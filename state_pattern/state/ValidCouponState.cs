using System;

namespace State
{
    public class ValidCouponState : ICouponState
    {
        public void ChangeExpiryDate(DateTime dateTime, ICoupon coupon)
        {

        }

        public bool UseDiscount()
        {
            return true;
        }
    }
    
}