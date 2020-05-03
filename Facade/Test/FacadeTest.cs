using System;
using Moq;
using FluentAssertions;
using Xunit;

namespace Facade.Test
{
    public class FacadeShould
    {
        private IEnvironment _environment = FacadeTestData.CreateMockEnvironment();
        private IMerchantAuthenticationType _merchantAuthenticationType = FacadeTestData.CreateMockMerchantAuthenticationType();
        private IBillingAddress _billingAddress = FacadeTestData.CreateMockBillingAddress();
        private ITransactionController _transactionController;

        private IPaymentProcessor CreatePaymentProcessor(DateTime creditCardExpiryDate)
        {
            var creditCard = FacadeTestData.CreateMockCreditCard(creditCardExpiryDate);
            var txnReq = FacadeTestData.CreateMockTransactionRequest(_billingAddress, creditCard);
            _transactionController = FacadeTestData.CreateMockTransactionController(txnReq, creditCard);
            return new PaymentProcessor(_environment, _merchantAuthenticationType, _transactionController);
        }
        [Fact]
        public void SetEnvironmentCorrectly_WhenFacadeInitializesPaymentGatewayInterface()
        {
            // Arrange
            var paymentProcessor = CreatePaymentProcessor(DateTime.Today.AddDays(1));
            // Act
            paymentProcessor.InitializePaymentGatewayInterface();
            // Assert
            _environment.environmentVariableTarget.Should().NotBe(EnvironmentTarget.UNINITIALIZED);
        }

        [Fact]
        public void MerchantAuthenticated_WhenFacadeInitializesPaymentGatewayInterface()
        {
            // Arrange
            var paymentProcessor = CreatePaymentProcessor(DateTime.Today.AddDays(1));
            // Act
            paymentProcessor.InitializePaymentGatewayInterface();
            // Assert
            using (new FluentAssertions.Execution.AssertionScope("merchant"))
            {
                _merchantAuthenticationType.LoginID.Should().NotBe(null);
                _merchantAuthenticationType.TransactionKey.Should().NotBe(null);
            }
        }

        [Fact]
        public void PaymentSubmittedSuccesfully_WhenFacadeInitializesCorrectlyAndChecksCreditCardExpiry()
        {
            // Arrange
            var paymentProcessor = CreatePaymentProcessor(DateTime.Today.AddDays(1));
            // Act
            paymentProcessor.InitializePaymentGatewayInterface();
            // Assert
            paymentProcessor.SubmitPayment().Should().BeTrue();
            using (new FluentAssertions.Execution.AssertionScope("transaction controller"))
            {
                var txnCtrl = Mock.Get(_transactionController);
                txnCtrl.Verify(tc => tc.Execute(), Times.Once);
                txnCtrl.Verify(ar => ar.GetApiResponse(), Times.Once);
            }
        }

        [Fact]
        public void PaymentSubmissionFails_WhenCreditCardIsExpired()
        {
            // Arrange
            var paymentProcessor = CreatePaymentProcessor(DateTime.Today.AddDays(-1));
            // Act
            paymentProcessor.InitializePaymentGatewayInterface();
            // Assert
            paymentProcessor.SubmitPayment().Should().BeFalse();
        }

    }
}
