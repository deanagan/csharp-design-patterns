namespace facade
{
    public interface IPaymentProcessor
    {
        void InitializePaymentGatewayInterface();
        bool SubmitPayment();
    }
}