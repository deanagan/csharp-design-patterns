using System;

namespace BuilderPattern
{
    public interface IDiscountStrategy
    {
        decimal CalculateDiscountedRetailPrice(IProduct product);

    }
}
