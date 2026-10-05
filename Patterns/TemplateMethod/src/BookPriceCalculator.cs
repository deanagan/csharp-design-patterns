using System;

namespace TemplateMethod
{
    public abstract class BookPriceCalculator
    {
        protected abstract decimal ComputeTotalPriceBeforeTax(IBook books);
        protected abstract decimal ApplyTax(decimal priceBeforeTax);

        public decimal CalculatePrice(IBook books)
        {
            var priceBeforeTax = ComputeTotalPriceBeforeTax(books);
            return ApplyTax(priceBeforeTax);
        }
    }
}
