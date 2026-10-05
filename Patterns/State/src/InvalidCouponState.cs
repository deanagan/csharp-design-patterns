using System;

namespace State
{
    public class InvalidCouponState : ICouponState
    {
        public void ChangeExpiryDate(DateTime dateTime, ICoupon coupon)
        {
            if (dateTime >= DateTime.Today)
            {
                coupon.SetCouponState(new ValidCouponState());
            }
        }

        public bool UseDiscount()
        {
            return false;
        }
    }
    
}