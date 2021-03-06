using System;

namespace State
{
    public interface ICoupon
    {
        void UpdateExpiryDate(DateTime dateTime);
        decimal ApplyDiscountTo(IProduct product);

        void SetCouponState(ICouponState state);
    }
    
}
