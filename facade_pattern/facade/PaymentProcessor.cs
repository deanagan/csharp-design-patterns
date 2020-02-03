using System;

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
            // Most likely a violation of the law of demeter. We shouldn't be
            // talking to the credit card class, but rather, to the txnRequest
            // which is a dependency. TO pass, we just do this for now.
            return _txnRequest.CreditCard.ExpiryDate.CompareTo(DateTime.Today) > 0;
        }
    }
}