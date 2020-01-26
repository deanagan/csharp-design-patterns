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
        public double CalculatePrice(IProduct product)
        {
            if (_coupon.IsExpired())
            {
                return product.SellingPrice();
            }
            var discount = product.OnSale() ? 0.0 : (product.SellingPrice() * _coupon.Discount());
            return product.SellingPrice() - discount;
        }
    }
}