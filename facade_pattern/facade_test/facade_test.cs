using System;
using NUnit.Framework;
using Moq;
using FluentAssertions;
using facade;

namespace facade_test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        IBillingAddress CreateMockBillingAddress()
        {
            return Mock.Of<IBillingAddress>
            (
                billingAddress =>
                    billingAddress.FirstName == "John" &&
                    billingAddress.LastName == "Wick" &&
                    billingAddress.Address == "W 43rd Street" &&
                    billingAddress.City == "New York" &&
                    billingAddress.ZipCode == "10036"
            );
        }

        ICreditCard CreateMockCreditCard(DateTime expiryDate)
        {
            return Mock.Of<ICreditCard>
            (
                creditCard =>
                    creditCard.Type == CreditCard.VISA &&
                    creditCard.AccountNumber == "123456789" &&
                    creditCard.CVC == "987" &&
                    creditCard.ExpiryDate == expiryDate
            );
        }

        IEnvironment CreateMockEnvironment()
        {
            return Mock.Of<IEnvironment>
            (
                environment => environment.environmentVariableTarget == EnvironmentTarget.SANDBOX
            );
        }

        IMerchantAuthenticationType CreateMockMerchantAuthenticationType()
        {
            return Mock.Of<IMerchantAuthenticationType>
            (
                merchAuthType => merchAuthType.LoginID == "Admin" &&
                                 merchAuthType.TransactionKey == "54321"
            );
        }

        ITransactionRequest CreateMockTransactionRequest(
            IBillingAddress billingAddress,
            ICreditCard creditCard)
        {
            return Mock.Of<ITransactionRequest>
            (
                txnReq => txnReq.Amount == 123.45M &&
                          txnReq.BillingAddress == billingAddress &&
                          txnReq.CreditCard == creditCard
            );
        }

        [Test]
        public void SubmitPaymentReturnsOK_UsingFacade()
        {
            // Arrange
            var paymentProcessor = new PaymentProcessor();

            // Act
            paymentProcessor.InitializePaymentGatewayInterface();

            // Assert
            paymentProcessor.SubmitPayment().Should().Be(true);
            
        }

    }
}