using System;

namespace BridgePattern
{
    public interface IOrder
    {
        void Checkout(decimal amount);
    }
}
