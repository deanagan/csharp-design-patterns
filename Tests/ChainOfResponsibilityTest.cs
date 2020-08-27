using System;
using Xunit;
using Moq;
using FluentAssertions;
using System.Collections.Generic;

namespace ChainOfResponsibility.Test
{
    public static class GatewayExtension
    {
        public static void IsCalledWith(this IPaymentGateway gateway, ICreditCard creditCard, Times times, ICreditCardHandler creditCardHandler)
        {
            Mock.Get(gateway)
                .Verify(paymentGateway => paymentGateway
                    .SubmitVerification(It.Is<ICreditCardHandler>(cch => cch.GetType() == creditCardHandler.GetType()), creditCard), times);
        }
    }

    public class ChainOfResponsibilityShould
    {
        private ICreditCardHandler _creditCardHandler;
        private IPaymentGateway _paymentGateway;

        private List<string> validStartingCardNumbers = new List<string>{ "3", "4", "5"};

        public ChainOfResponsibilityShould()
        {
            _paymentGateway = Mock.Of<IPaymentGateway>();
            _creditCardHandler = new VisaCardHandler(_paymentGateway);
            _creditCardHandler.SetNext(new AmexCardHandler(_paymentGateway))
                              .SetNext(new MastercardHandler(_paymentGateway));
        }

        private ICreditCard PrepareCreditCardPayment(string cardNumber)
        {
            Mock.Get(_paymentGateway)
                .Setup(gateway => gateway
                .SubmitVerification(It.IsAny<ICreditCardHandler>(), It.IsAny<ICreditCard>()))
		        .Returns(validStartingCardNumbers.Contains(cardNumber.Substring(0,1)));
            return Mock.Of<ICreditCard>(creditCard => creditCard.Number == cardNumber);
        }

        public delegate ICreditCardHandler CreditCardHandlerCreator(IPaymentGateway gateway);
        public static IEnumerable<object[]> ValidCreditCardNumbers
        {
            get
            {
                yield return new object[] {new CreditCardHandlerCreator((gateway) => new VisaCardHandler(gateway)), "41263434" };
                yield return new object[] {new CreditCardHandlerCreator((gateway) => new AmexCardHandler(gateway)), "341263434" };
                yield return new object[] {new CreditCardHandlerCreator((gateway) => new MastercardHandler(gateway)), "5384683" };
            }
        }

        [Theory]
        [MemberData(nameof(ValidCreditCardNumbers))]
        public void BeValidWithCorrectCardType_WhenCreditCardSubmittedForVerification(CreditCardHandlerCreator creditCardHandlerCreator, string cardNumber)
        {
            // Arrange
            var creditCard = PrepareCreditCardPayment(cardNumber);
            var expectedCreditCardHandler = creditCardHandlerCreator(_paymentGateway);

            // Act
            var isValid = _creditCardHandler.IsCreditCardValid(creditCard);

            // Assert
            using (new FluentAssertions.Execution.AssertionScope("credit card"))
            {
                isValid.Should().BeTrue();
                _paymentGateway.IsCalledWith(creditCard, Times.Once(), expectedCreditCardHandler);
            }
        }

        [Fact]
        public void NotSubmitPayments_WhenInvalidCardSubmittedForVerification()
        {
            // Arrange
            var invalidCreditCard = PrepareCreditCardPayment("9244683");

            // Act
            var isValid = _creditCardHandler.IsCreditCardValid(invalidCreditCard);

            // Assert
            using (new FluentAssertions.Execution.AssertionScope("credit card"))
            {
                isValid.Should().BeFalse();
                _paymentGateway.IsCalledWith(It.IsAny<ICreditCard>(), Times.Never(), It.IsAny<ICreditCardHandler>());
            }
        }
    }
}
