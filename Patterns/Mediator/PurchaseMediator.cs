using System;
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
        public bool BroadcastPurchaseCompletion(IPurchaser purchaser)
        {
            var isPurchaserActive = _activePurchasers.Remove(purchaser);

            if (isPurchaserActive)
            {
                _activePurchasers.ForEach(p => p.Receive(purchaser));
            }

            return isPurchaserActive;
        }
        public bool AddPurchaser(IPurchaser purchaser)
        {
            if (_activePurchasers.Contains(purchaser) == false)
            {
                _activePurchasers.Add(purchaser);
                return true;
            }

            return false;
        }
    }
}