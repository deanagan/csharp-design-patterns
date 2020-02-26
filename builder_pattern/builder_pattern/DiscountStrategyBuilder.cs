using System;

namespace BuilderPattern
{
    public abstract class DiscountStrategyBuilder
    {
        private IDiscountStrategy _discountStrategy;

        public DiscountStrategyBuilder(IDiscountStrategy discountStrategy)
        {
            _discountStrategy = discountStrategy;
        }

        DiscountStrategyBuilder ApplicableToSKUCodeStartingWith(string skuCodeStart)
        {
            _discountStrategy.SkuCodeStart = skuCodeStart;
            return this;
        }

        DiscountStrategyBuilder WithDiscount(int discountInPercentage)
        {
            _discountStrategy.DiscountInPercentage = discountInPercentage;
            return this;
        }
        IDiscountStrategy Build()
        {
            return _discountStrategy;
        }


    }
}
