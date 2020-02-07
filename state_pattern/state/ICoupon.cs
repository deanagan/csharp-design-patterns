using System;

namespace State
{
    public interface ICoupon
    {
        void UpdateExpiryDate(DateTime dateTime);
        decimal FinalSellingPrice(IProduct product);
    }
    
}
