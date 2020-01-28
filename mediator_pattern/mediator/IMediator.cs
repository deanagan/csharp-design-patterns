using System;

namespace Mediator
{
    public interface IMediator
    {
        void BroadcastPurchaseCompletion(IPurchaser purchaser);
        void AddPurchaser(IPurchaser purchaser);
    }
}
