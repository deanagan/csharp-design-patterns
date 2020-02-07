
using System;

namespace State
{
    public interface ICouponState
    {
        void ChangeExpiryDate(DateTime date, ICoupon coupon);
        decimal GetDiscount();
    }
    
}