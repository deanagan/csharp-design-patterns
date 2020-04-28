using System;

namespace Bridge
{
    public abstract class Order
    {
        protected IPayment Payment { get; }
        public Order(IPayment payment)
        {
            Payment = payment;
        }
        public abstract void Checkout(decimal amount);
    }
}
