using System;

namespace Mediator
{
    public interface IPurchaser
    {
        string Bought();
        string Location();
        void Receive(IPurchaser purchaser);
        void Complete();
    }
}
