using System;

namespace Bridge;

public class PurchaseOrder(IPayment payment) : Order(payment)
{
    public override void Checkout(decimal amount)
    {
        Payment.SubmitPayment(amount);
    }
}
