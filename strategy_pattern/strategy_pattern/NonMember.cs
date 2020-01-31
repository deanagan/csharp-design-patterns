using System;


namespace Strategy
{
    public class NonMember : ICustomer
    {
        private readonly ICoupon _coupon;
        public NonMember(ICoupon coupon)
        {
            _coupon = coupon;
        }
        public bool IsMember()
        {
            return false;
        }
        public decimal Price(IProduct product)
        {
            if (_coupon.IsExpired())
            {
                return product.SellingPrice();
            }
            var discount = product.IsOnSale() ? 0M : (product.SellingPrice() * (_coupon.DiscountPercentage()/ 100M));
            return product.SellingPrice() - discount;
        }

        public void ComputePrice()
        {
            
        }
    }
}