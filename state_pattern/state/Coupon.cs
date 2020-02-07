using System;

namespace State
{
    public class Coupon : ICoupon
    {
        public Coupon(int discount)
        {

        }

        public void UpdateExpiryDate(DateTime dateTime)
        {

        }
        
        public decimal FinalSellingPrice(IProduct product)
        {
            return 0;
        }
    }
    
}