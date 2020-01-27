using System;


namespace Strategy
{
    public interface IProduct
    {
        Decimal SellingPrice();

        bool OnSale();
    }
}