using System;

namespace Bridge
{
    public interface IPayment
    {
        void SubmitPayment(decimal amount);
    }
}
