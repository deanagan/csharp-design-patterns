
using System;

namespace State
{
    public interface ICouponState
    {
        void ChangeExpiryDate(DateTime date);
        decimal GetDiscount();
    }
    
}