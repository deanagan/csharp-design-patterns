using System;

namespace Strategy
{
    public interface ICustomer
    {
        bool IsMember();
        Decimal CalculatePrice(IProduct product);
    }
}