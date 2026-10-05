using System;

namespace Mediator
{
    public interface IPurchaser
    {
        void Receive(IPurchaser purchaser);
        void Complete(Product product);
        Product? GetProduct();
    }
}
