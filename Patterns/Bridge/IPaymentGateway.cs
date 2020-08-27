using System;

namespace Bridge
{
    public interface IPaymentGateway
    {
        void ProcessPayment(decimal amount, IPayment payment);
    }
}
