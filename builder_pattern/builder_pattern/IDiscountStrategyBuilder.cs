using System;

namespace BuilderPattern
{
    public interface IDiscountStrategyBuilder
    {
        IDiscountStrategyBuilder ApplicableToSKUCodeStartingWith(string skuBeginning);
        IDiscountStrategyBuilder WithDiscount(int discountInPercentage);
        IDiscountStrategy Build();
    }
}
