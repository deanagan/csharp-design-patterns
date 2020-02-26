using System;

namespace BuilderPattern
{
    public interface IDiscountStrategy
    {
        int Discount { get; set; }
        string SkuCodeStart {get; set;}
        decimal CalculateDiscountedRetailPrice(Product product);

    }
}
