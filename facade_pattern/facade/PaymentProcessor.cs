namespace facade
{
    public class PaymentProcessor : IPaymentProcessor
    {
        private readonly IEnvironment _environment;
        public PaymentProcessor(IEnvironment environment)
        {
            _environment = environment;
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