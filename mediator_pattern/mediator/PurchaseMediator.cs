using System.Collections.Generic;


namespace Mediator
{
    public class PurchaseMediator : IMediator
    {
        private List<IPurchaser> _activePurchasers;

        public PurchaseMediator()
        {
            _activePurchasers = new List<IPurchaser>();
        }
        public void BroadcastPurchaseCompletion(IPurchaser purchaser)
        {
            _activePurchasers.Remove(purchaser);
            _activePurchasers.ForEach(p => p.Receive(purchaser));
        }
        public void AddPurchaser(IPurchaser purchaser)
        {
            _activePurchasers.Add(purchaser);
        }
    }
}