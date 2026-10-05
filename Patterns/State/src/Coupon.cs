using System;
using System.Collections.Generic;

namespace State
{
    public class Coupon : ICoupon
    {
        private int _discount;
        private ICouponState _currentState;
        private DateTime _expiry;
        
        public Coupon(int discount, DateTime expiry)
        {
            if (expiry < DateTime.Today)
            {
                throw new ArgumentException("Coupon must be constructed with valid expiry date");
            }
            _expiry = expiry;
            _discount = discount;
            // Because coupon states are internal, we will
            // couple them with the specific states so they
            // are highly cohesive. For this implementation,
            // we will construct on state change.
            _currentState = new ValidCouponState();
        }

        public void SetCouponState(ICouponState state)
        {
            _currentState = state;
        }

        public void UpdateExpiryDate(DateTime dateTime)
        {
            _currentState.ChangeExpiryDate(dateTime, this);
        }
        
        public decimal ApplyDiscountTo(IProduct product)
        {
            var price = product.Price;
            return _currentState.UseDiscount() ? price - (price * (_discount/100.0M)) : price;
        }
    }
    
}