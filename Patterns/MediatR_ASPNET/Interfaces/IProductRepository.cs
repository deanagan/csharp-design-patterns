using System.Collections.Generic;

using Api.Models;

namespace Api.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
    }
}