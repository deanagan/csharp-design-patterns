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

        public DiscountStrategyBuilder ApplicableToSKUCode(string skuCode)
        {
            _discountStrategy.SkuCode = skuCode;
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
