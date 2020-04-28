using NUnit.Framework;
using Moq;
using FluentAssertions;

namespace BridgePattern.Test
{
    public class Tests
    {
        private IPaymentGateway _mPaymentGateway;
        [SetUp]
        public void Setup()
        {
            _mPaymentGateway = Mock.Of<IPaymentGateway>();
        }

        private void VerifyPaymentMethod<T>(decimal amount)
        {
            Mock.Get(_mPaymentGateway).Verify(
                pg => pg.ProcessPayment(20.0M, It.Is<IPayment>
                    (p => p.GetType() == typeof(T))), Times.Once());

        }

        [Test]
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

         [Test]
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
