using System.Collections.Generic;
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

        public delegate IPayment PaymentMethodCreator(IPaymentGateway gateway);

        public static IEnumerable<object[]> PaymentMethodCreators
        {
            get
            {
                yield return new object[] { new PaymentMethodCreator((gateway) => new CreditCardPayment(gateway)) };
                yield return new object[] { new PaymentMethodCreator((gateway) => new PaypalPayment(gateway)) };
            }
        }

        [Theory]
        [MemberData(nameof(PaymentMethodCreators))]
        public void UseCorrectPaymentMethod_WhenCheckingOut(PaymentMethodCreator paymentMethodCreator)
        {
            // Arrange
            var paymentMethod = paymentMethodCreator(_mPaymentGateway);
            var order = new PurchaseOrder(paymentMethod);

            // Act
            order.Checkout(20.0M);

            // Assert
            using (new FluentAssertions.Execution.AssertionScope("payments"))
            {
                Mock.Get(_mPaymentGateway).Verify(
                    pp => pp.ProcessPayment(20.0M,
                    It.Is<IPayment>(p => p.GetType() == paymentMethod.GetType())), Times.Once);

                paymentMethod.GetType().Should().Implement<IPayment>();
            }
        }

    }
}
