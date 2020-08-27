namespace Facade
{
    public interface IPaymentProcessor
    {
        void InitializePaymentGatewayInterface();
        bool SubmitPayment();
    }
}
