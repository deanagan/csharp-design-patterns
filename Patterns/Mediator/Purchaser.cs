using System;

namespace Mediator
{
    public class Purchaser : IPurchaser
    {
        public string ItemBought  {get; private set;}
        public string Location  {get; private set;}
        private Product _product;
        private IAlertScreen _alertScreen;
        private IMediator _mediator;
        public Purchaser(IAlertScreen alertScreen, IMediator mediator)
        {
            if (alertScreen == null)
            {
                throw new ArgumentNullException("IAlertScreen is null");
            }

            if (mediator == null)
            {
                throw new ArgumentNullException("IMediator is null");
            }

            _alertScreen = alertScreen;
            _mediator = mediator;
        }

        public void Receive(IPurchaser purchaser)
        {
            var product = purchaser.GetProduct();
            _alertScreen.ShowMessage(product.Item, product.Location);
        }

        public void Complete(Product product)
        {
            _product = product;
            _mediator.BroadcastPurchaseCompletion(this);
        }

        public Product GetProduct()
        {
            return _product;
        }
    }
}