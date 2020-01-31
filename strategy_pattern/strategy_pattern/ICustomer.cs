using System;

namespace Strategy
{
    public interface ICustomer
    {
        bool IsMember();
        decimal Price(IProduct product);

        void ComputePrice();
    }
}