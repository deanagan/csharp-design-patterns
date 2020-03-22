using System;

namespace BridgePattern
{
    public interface IPaymentGateway
    {
        void ProcessPayment(decimal amount, IPayment payment);
    }
}
