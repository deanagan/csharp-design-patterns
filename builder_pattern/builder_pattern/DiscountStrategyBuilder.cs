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

        public DiscountStrategyBuilder ApplicableToSKUCodeStartingWith(string skuCodeStart)
        {
            _discountStrategy.SkuCodeStart = skuCodeStart;
            return this;
        }

        public DiscountStrategyBuilder WithDiscountInPercentage(int discountInPercentage)
        {
            _discountStrategy.DiscountInPercentage = discountInPercentage;
            return this;
        }
        public IDiscountStrategy Build()
        {
            return _discountStrategy;
        }


    }
}
