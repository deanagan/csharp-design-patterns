using System;


namespace Strategy
{
    public interface IProduct
    {
        double SellingPrice();

        bool OnSale();
    }
}