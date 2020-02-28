namespace ChainOfResponsibility
{
    public abstract class DiscountCalculatorBase : IDiscountCalculator
    {
        private IDiscountCalculator _nextDiscountCalculator;
        public IDiscountCalculator SetNextCalculator(IDiscountCalculator discountCalculator)
        {
            this._nextDiscountCalculator = discountCalculator;
            return discountCalculator;
        }
        public decimal Calculate(Product product)
        {
            if (_nextDiscountCalculator != null)
            {
                return _nextDiscountCalculator.Calculate(product);
            }

            return product.Price;
        }
    }
}