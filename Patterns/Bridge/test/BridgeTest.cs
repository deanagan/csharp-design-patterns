using Bridge;
using NSubstitute;
using Xunit;

namespace BridgeTest;

public class BridgeShould
{
    private readonly IPaymentGateway _paymentGateway;

    public BridgeShould()
    {
        _paymentGateway = Substitute.For<IPaymentGateway>();
    }

    public delegate IPayment PaymentMethodCreator(IPaymentGateway gateway);

    public static TheoryData<PaymentMethodCreator> PaymentMethodCreators => new()
    {
        new PaymentMethodCreator(gateway => new CreditCardPayment(gateway)),
        new PaymentMethodCreator(gateway => new PaypalPayment(gateway))
    };

    [Theory]
    [MemberData(nameof(PaymentMethodCreators))]
    public void SubmitPaymentToGateway_WhenCheckingOut(PaymentMethodCreator paymentMethodCreator)
    {
        // Arrange
        var paymentMethod = paymentMethodCreator(_paymentGateway);
        var order = new PurchaseOrder(paymentMethod);
        const decimal amount = 20.0M;

        // Act
        order.Checkout(amount);

        // Assert
        _paymentGateway.Received(1).ProcessPayment(amount, paymentMethod);
    }
}
