using System;

namespace BridgePattern
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
