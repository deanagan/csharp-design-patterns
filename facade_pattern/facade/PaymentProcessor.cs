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
            // TODO: Add data invariance. Use contracts?
            _environment = environment;
            _merchAuthType = merchAuthType;
            _txnRequest = txnRequest;
        }
        public void InitializePaymentGatewayInterface()
        {
            _environment.environmentVariableTarget = EnvironmentTarget.SANDBOX;
            _merchAuthType.TransactionKey = "transaction_key";
            _merchAuthType.LoginID = "login_id";
        }
        public bool SubmitPayment()
        {
            return false;
        }
    }
}