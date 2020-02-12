namespace TemplateMethod
{
    public class HighIncomeBookPriceCalculator : BookPriceCalculator
    {
        protected override decimal ComputeTotalPriceBeforeTax(IBook books)
        {
            return 0.0M;
        }
        protected override decimal ApplyTax(decimal priceBeforeTax)
        {
            return 0.0M;
        }
    }
}