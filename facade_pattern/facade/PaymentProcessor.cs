namespace facade
{
    public class PaymentProcessor : IPaymentProcessor
    {
        private readonly IEnvironment _environment;
        private readonly IMerchantAuthenticationType _merchAuthType;
        private readonly ITransactionRequest _txnRequest;
        public PaymentProcessor(
            IEnvironment environment, 
            IMerchantAuthenticationType merchAuthType,
            ITransactionRequest txnRequest)
        {
            _environment = environment;
            _merchAuthType = merchAuthType;
            _txnRequest = txnRequest;
        }
        public void InitializePaymentGatewayInterface()
        {
            _environment.environmentVariableTarget = EnvironmentTarget.SANDBOX;
        }
        public bool SubmitPayment()
        {
            return false;
        }
    }
}