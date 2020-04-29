using System;

namespace Builder
{
    public interface IDiscountStrategy
    {
        int DiscountInPercentage { get; set; }
        string SkuCode {get; set;}
        decimal CalculateDiscountedRetailPrice(Product product);

    }
}
