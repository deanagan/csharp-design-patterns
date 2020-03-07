using NUnit.Framework;
using Moq;
using System;
using FluentAssertions;
using System.Collections.Generic;

namespace ChainOfResponsibility.Tests
{
    public class Tests
    {
        private ICreditCardHandler creditCardHandler;
        private Mock<IPaymentGateway> paymentGatewayMock;

        private List<string> validStartingCardNumbers = new List<string>{ "3", "4", "5"};

        [SetUp]
        public void Setup()
        {
            paymentGatewayMock = new Mock<IPaymentGateway>();
            creditCardHandler = new VisaCardHandler(paymentGatewayMock.Object);
            creditCardHandler.SetNext(new AmexCardHandler(paymentGatewayMock.Object))
                             .SetNext(new MastercardHandler(paymentGatewayMock.Object));
        }
        

        private ICreditCard PrepareCreditCardPayment(string cardNumber)
        {
            paymentGatewayMock
                .Setup(gateway => gateway
                .SubmitVerification(It.IsAny<ICreditCardHandler>(), It.IsAny<ICreditCard>()))
		        .Returns(validStartingCardNumbers.Contains(cardNumber.Substring(0,1)));
            return Mock.Of<ICreditCard>(creditCard => creditCard.Number == cardNumber);
        }

        [Test]
        public void AmexCardHandlerUsed_WhenAmexCardSubmittedForVerification()
        {
            // Arrange
            var amexCreditCard = PrepareCreditCardPayment("341263434");
            
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
            var visaCreditCard = PrepareCreditCardPayment("41263434");

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
            var masterCreditCard = PrepareCreditCardPayment("5384683");

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
            var invalidCreditCard = PrepareCreditCardPayment("9244683");

            // Act
            var isValid = creditCardHandler.IsCreditCardValid(invalidCreditCard);

            // Assert
            isValid.Should().BeFalse();
            paymentGatewayMock
                .Verify(paymentGateway => paymentGateway
                .SubmitVerification(It.IsAny<ICreditCardHandler>(), It.IsAny<ICreditCard>()), Times.Never);

        }
    }
}