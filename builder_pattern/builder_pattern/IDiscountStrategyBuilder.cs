using System;

namespace BuilderPattern
{
    public interface IDiscountStrategyBuilder
    {
        IDiscountStrategyBuilder WithBeginningSKUCode(string skuBeginning);
        IDiscountStrategyBuilder WithDiscountValuedAt(int discountInPercentage);
        IDiscountStrategy Build();
    }
}
