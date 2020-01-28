using System;

namespace Mediator
{
    public interface IPurchaser
    {
        string Bought(); // called by the other purchaser
        string Location(); // called by other purchaser
        void Receive(IPurchaser purchaser); // called by mediator to notify what other purchaser bought.

        void Complete(IPurchaser purchaser); // called by client to complete a purchase
    }
}
