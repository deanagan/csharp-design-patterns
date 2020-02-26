using System;

namespace BuilderPattern
{
    public interface IDiscountStrategy
    {
        int DiscountInPercentage { get; set; }
        string SkuCodeStart {get; set;}
        decimal CalculateDiscountedRetailPrice(Product product);

    }
}
