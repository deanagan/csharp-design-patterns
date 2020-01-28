using System;

namespace Mediator
{
    public interface IMediator
    {
        void BroadcastPurchaseCompletion(IPurchaser purchaser);
        void AddPurchaser(IPurchaser purchaser);
    }
}


// IMediator = new Mediator;
// Purchaser p1 = new Purchaser();
// Purchaser p2 = new Purchaser();



// p1.Notify("ball", "sydney");
//     Mediator.PurchaseAlert(p1);

// px.OtherPurchaser()
