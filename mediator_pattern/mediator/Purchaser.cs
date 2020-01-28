namespace Mediator
{
    public class Purchaser : IPurchaser
    {
        private IAlertScreen _alertScreen;
        private IMediator _mediator;
        public Purchaser(IAlertScreen alertScreen, IMediator mediator)
        {
            _alertScreen = alertScreen;
            _mediator = mediator;
        }

        public string Bought()
        {
            return "Ball";
        }
        public string Location()
        {
            return "Sydney";
        }

        public void Receive(IPurchaser purchaser)
        {
            _alertScreen.ShowMessage(purchaser.Bought(), purchaser.Location());
        }

        public void Complete()
        {
            _mediator.BroadcastPurchaseCompletion(this);
        }
    }
}