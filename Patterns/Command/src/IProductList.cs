using System.Collections.Generic;

namespace Command
{
    public interface IProductList
    {
        string Name { get; set; }
        List<IProduct> Products { get; set;}
    }
}