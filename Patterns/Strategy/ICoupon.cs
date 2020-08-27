using System;

namespace Strategy
{
    public interface ICoupon
    {
        int DiscountPercentage();
        bool IsExpired();
    }
}
