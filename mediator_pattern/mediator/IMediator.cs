using System;

namespace Mediator
{
    public interface IMediator
    {
        bool BroadcastPurchaseCompletion(IPurchaser purchaser);
        bool AddPurchaser(IPurchaser purchaser);
    }
}
