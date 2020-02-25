using System;

namespace BuilderPattern
{
    public interface IProduct
    {
        string StockKeepingUnit { get; set; }
        decimal RegularRetailPrice { get; set; }
    }
}
