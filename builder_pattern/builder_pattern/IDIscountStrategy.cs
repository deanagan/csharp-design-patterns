using System;

namespace BuilderPattern
{
    public interface IDiscountStrategy
    {
        int DiscountInPercentage { get; set; }
        string SkuCode {get; set;}
        decimal CalculateDiscountedRetailPrice(Product product);

    }
}
