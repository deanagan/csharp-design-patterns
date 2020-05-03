using System;

namespace Facade
{
    public class PaymentProcessor : IPaymentProcessor
    {
        private readonly IEnvironment _environment;
        private readonly IMerchantAuthenticationType _merchAuthType;
        private readonly ITransactionController _txnCtrl;
        public PaymentProcessor(
            IEnvironment environment,
            IMerchantAuthenticationType merchAuthType,
            ITransactionController txnCtrl)
        {
            // TODO: Add data invariance. Use contracts?
            _environment = environment;
            _merchAuthType = merchAuthType;
            _txnCtrl = txnCtrl;
        }
        public void InitializePaymentGatewayInterface()
        {
            _environment.environmentVariableTarget = EnvironmentTarget.SANDBOX;
            _merchAuthType.TransactionKey = "transaction_key";
            _merchAuthType.LoginID = "login_id";
        }
        public bool SubmitPayment()
        {
            _txnCtrl.Execute();
            var t =  _txnCtrl.GetApiResponse() == TransactionResponseType.OK;
            return t;
        }
    }
}
