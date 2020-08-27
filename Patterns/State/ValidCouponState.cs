using System;

namespace State
{
    public class ValidCouponState : ICouponState
    {
        public void ChangeExpiryDate(DateTime dateTime, ICoupon coupon)
        {
            if (dateTime < DateTime.Today)
            {
                coupon.SetCouponState(new InvalidCouponState());
            }
            
        }

        public bool UseDiscount()
        {
            return true;
        }
    }
    
}