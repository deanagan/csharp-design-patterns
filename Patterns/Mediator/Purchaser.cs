using System;

namespace Mediator
{
    public class Purchaser : IPurchaser
    {
        public string ItemBought { get; private set; } = string.Empty;
        public string Location { get; private set; } = string.Empty;
        private Product? _product;
        private IAlertScreen _alertScreen;
        private IMediator _mediator;

        public Purchaser(IAlertScreen? alertScreen, IMediator? mediator)
        {
            ArgumentNullException.ThrowIfNull(alertScreen);
            ArgumentNullException.ThrowIfNull(mediator);

            _alertScreen = alertScreen;
            _mediator = mediator;
        }

        public void Receive(IPurchaser purchaser)
        {
            var product = purchaser.GetProduct();
            if (product != null)
            {
                _alertScreen.ShowMessage(product.Item, product.Location);
            }
        }

        public void Complete(Product product)
        {
            _product = product;
            _mediator.BroadcastPurchaseCompletion(this);
        }

        public Product? GetProduct()
        {
            return _product;
        }
    }
}