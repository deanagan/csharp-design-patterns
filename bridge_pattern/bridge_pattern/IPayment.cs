using System;

namespace BridgePattern
{
    public interface IPayment
    {
        void SubmitPayment(decimal amount);
    }
}
