using System.Collections.Generic;
using Api.Models;

namespace Api.Interfaces
{
    public interface IProductService
    {
        Product GetProduct(string skuCode);
    }
}
