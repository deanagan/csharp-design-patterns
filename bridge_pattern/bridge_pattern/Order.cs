using System;

namespace BridgePattern
{
    public class Order : IOrder
    {
        private readonly IPayment _mPayment;
        public Order(IPayment payment)
        {
            _mPayment = payment;
        }
        public void Checkout(decimal amount)
        {
            _mPayment.SubmitPayment(amount);
        }
    }
}
