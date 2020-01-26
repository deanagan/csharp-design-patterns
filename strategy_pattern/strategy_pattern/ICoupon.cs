using System;

namespace Strategy
{
    public interface ICoupon
    {
        double Discount();
        bool IsExpired();
    }
}
