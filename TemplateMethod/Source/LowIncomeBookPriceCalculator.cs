namespace TemplateMethod
{
    public class LowIncomeBookPriceCalculator : BookPriceCalculator
    {
        private readonly int _discount;
        public LowIncomeBookPriceCalculator(int discount)
        {
            _discount = discount;
        }
        protected override decimal ComputeTotalPriceBeforeTax(IBook books)
        {
            return books.Price - (books.Price * (_discount / 100.0M));
        }
        protected override decimal ApplyTax(decimal priceBeforeTax)
        {
            return priceBeforeTax;
        }
    }
}