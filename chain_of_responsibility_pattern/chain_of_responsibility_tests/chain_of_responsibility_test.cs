using NUnit.Framework;
using Moq;
using System;
using FluentAssertions;

namespace ChainOfResponsibility.Tests
{
    public class Tests
    {
        private ICreditCardHandler creditCardHandler;
        private Mock<IPaymentGateway> paymentGatewayMock;
        [SetUp]
        public void Setup()
        {
            paymentGatewayMock = new Mock<IPaymentGateway>();
            creditCardHandler = new VisaCardHandler(paymentGatewayMock.Object);
            creditCardHandler.SetNext(new AmexCardHandler(paymentGatewayMock.Object))
                             .SetNext(new MastercardHandler(paymentGatewayMock.Object));
        }


        [Test]
        public void AmexCardHandlerUsed_WhenAmexCardSubmittedForVerification()
        {
            // Arrange
            var amexCreditCard = Mock.Of<ICreditCard>(creditCard => creditCard.Number == "341263434");
            paymentGatewayMock
                .Setup(gateway => gateway
                    .SubmitVerification(It.IsAny<ICreditCardHandler>(), It.IsAny<ICreditCard>()))
		        .Returns((ICreditCardHandler cch, ICreditCard cc) => cc.Number.StartsWith("3") && 
                                                                     cch.GetType() == typeof(AmexCardHandler));

            // Act
            var isValid = creditCardHandler.IsCreditCardValid(amexCreditCard);

            // Assert
            isValid.Should().BeTrue();
            paymentGatewayMock
                .Verify(paymentGateway => paymentGateway
                .SubmitVerification(It.IsAny<AmexCardHandler>(), 
                                    amexCreditCard), Times.Once);

        }

        [Test]
        public void VisaCardHandlerUsed_WhenVisaCardSubmittedForVerification()
        {
            // Arrange
            var visaCreditCard = Mock.Of<ICreditCard>(creditCard => creditCard.Number == "41263434");
            paymentGatewayMock
                .Setup(gateway => gateway
                .SubmitVerification(It.IsAny<ICreditCardHandler>(), It.IsAny<ICreditCard>()))
		        .Returns((ICreditCardHandler cch, ICreditCard cc) => cc.Number.StartsWith("4") && 
                                                                     cch.GetType() == typeof(VisaCardHandler));

            // Act
            var isValid = creditCardHandler.IsCreditCardValid(visaCreditCard);

            // Assert
            isValid.Should().BeTrue();
            paymentGatewayMock
                .Verify(paymentGateway => paymentGateway
                .SubmitVerification(It.IsAny<VisaCardHandler>(), 
                                    visaCreditCard), Times.Once);

        }


        [Test]
        public void MasterCardHandlerUsed_WhenVisaCardSubmittedForVerification()
        {
            // Arrange
            var masterCreditCard = Mock.Of<ICreditCard>(creditCard => creditCard.Number == "5384683");
            paymentGatewayMock
                .Setup(gateway => gateway
                .SubmitVerification(It.IsAny<ICreditCardHandler>(), It.IsAny<ICreditCard>()))
		        .Returns((ICreditCardHandler cch, ICreditCard cc) => cc.Number.StartsWith("5") && 
                                                                     cch.GetType() == typeof(MastercardHandler));

            // Act
            var isValid = creditCardHandler.IsCreditCardValid(masterCreditCard);

            // Assert
            isValid.Should().BeTrue();
            paymentGatewayMock
                .Verify(paymentGateway => paymentGateway
                .SubmitVerification(It.IsAny<MastercardHandler>(), 
                                    masterCreditCard), Times.Once);

        }

        [Test]
        public void CardHandlerNull_WhenInvalidCardSubmittedForVerification()
        {
            // Arrange
            var creditCard = Mock.Of<ICreditCard>(creditCard => creditCard.Number == "9244683");

            paymentGatewayMock
                .Setup(gateway => gateway
                .SubmitVerification(It.IsAny<ICreditCardHandler>(), It.IsAny<ICreditCard>()))
		        .Returns(false);

            // Act
            var isValid = creditCardHandler.IsCreditCardValid(creditCard);

            // Assert
            isValid.Should().BeFalse();
            paymentGatewayMock
                .Verify(paymentGateway => paymentGateway
                .SubmitVerification(It.IsAny<ICreditCardHandler>(), It.IsAny<ICreditCard>())
                                    , Times.Never);

        }
    }
}