using System;


namespace Strategy
{
    public interface IProduct
    {
        decimal SellingPrice();

        bool IsOnSale();
    }
}