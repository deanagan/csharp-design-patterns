using System;

namespace Strategy
{
    public interface ICustomer
    {
        bool IsMember();
        double CalculatePrice(IProduct product);
    }
}