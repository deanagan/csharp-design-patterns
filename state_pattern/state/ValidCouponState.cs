using System;

namespace State
{
    public class ValidCouponState : ICouponState
    {
        public void ChangeExpiryDate(DateTime dateTime)
        {

        }

        public decimal GetDiscount()
        {
            return 0.0M;
        }
    }
    
}