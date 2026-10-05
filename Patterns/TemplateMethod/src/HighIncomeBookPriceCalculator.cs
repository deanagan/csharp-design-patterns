namespace TemplateMethod
{
    public class HighIncomeBookPriceCalculator : BookPriceCalculator
    {
        private static readonly int MTaxPercentage = 12;
        private readonly int _discount;
        public HighIncomeBookPriceCalculator(int discountPercentage)
        {
            _discount = discountPercentage;
        }
        protected override decimal ComputeTotalPriceBeforeTax(IBook books)
        {
            return books.Price - (books.Price * (_discount / 100.0M));
        }
        protected override decimal ApplyTax(decimal priceBeforeTax)
        {
            return priceBeforeTax + (priceBeforeTax * (MTaxPercentage / 100.0M));
        }
    }
}