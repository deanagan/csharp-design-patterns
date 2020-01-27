using System;

namespace Strategy
{
    public interface ICustomer
    {
        bool IsMember();
        Decimal Price(IProduct product);
    }
}