using Xunit;
using Moq;
using FluentAssertions;

namespace Bridge.Test
{
    public class BridgeShould
    {
        private IPaymentGateway _mPaymentGateway;

        public BridgeShould()
        {
            _mPaymentGateway = Mock.Of<IPaymentGateway>();
        }

        private void VerifyPaymentMethod<T>(decimal amount)
        {
            Mock.Get(_mPaymentGateway).Verify(
                pg => pg.ProcessPayment(20.0M, It.Is<IPayment>
                    (p => p.GetType() == typeof(T))), Times.Once());

        }

        [Fact]
        public void CreditCardPaymentReceivedWhenCheckingOutWithCreditCard()
        {
            // Arrange
            var paymentMethod = new CreditCardPayment(_mPaymentGateway);
            var order = new PurchaseOrder(paymentMethod);

            // Act
            order.Checkout(20.0M);

            // Assert
            VerifyPaymentMethod<CreditCardPayment>(20.0M);
        }

        [Fact]
        public void PaypalPaymentReceivedWhenCheckingOutWithPaypal()
        {
            // Arrange
            var paymentMethod = new PaypalPayment(_mPaymentGateway);
            var order = new PurchaseOrder(paymentMethod);

            // Act
            order.Checkout(20.0M);

            // Assert
            VerifyPaymentMethod<PaypalPayment>(20.0M);
        }
    }
}
