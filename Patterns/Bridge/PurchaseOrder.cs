using System;

namespace Bridge
{
    public class PurchaseOrder : Order
    {
        public PurchaseOrder(IPayment payment) : base(payment)
        {
        }
        public override void Checkout(decimal amount)
        {
            Payment.SubmitPayment(amount);
        }
    }
}
