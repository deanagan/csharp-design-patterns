using System;
using System.Collections.Generic;

namespace State
{
    public class Coupon : ICoupon
    {
        private int _discount;
        private ICouponState _validState;
        private ICouponState _invalidState;
        private ICouponState _currentState;
        private DateTime _expiry;
        
        public Coupon(int discount, DateTime expiry)
        {
            _expiry = expiry;
            _discount = discount;
            // Because coupon states are internal, we will
            // couple them with the specific states so they
            // are highly cohesive.
            _validState = new ValidCouponState();
            _invalidState = new InvalidCouponState();
            _currentState = _validState;
        }

        public void UpdateExpiryDate(DateTime dateTime)
        {

        }
        
        public decimal FinalSellingPrice(IProduct product)
        {
            var price = product.Price;
            return price - (price * (_discount/100.0M));
        }
    }
    
}