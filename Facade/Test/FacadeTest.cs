using System;
using Moq;
using FluentAssertions;
using Xunit;

namespace Facade.Test
{
    public class FacadeShould
    {
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
                environment => environment.environmentVariableTarget == EnvironmentTarget.UNINITIALIZED
            );
        }

        IMerchantAuthenticationType CreateMockMerchantAuthenticationType()
        {
            return Mock.Of<IMerchantAuthenticationType>
            (
                merchAuthType => merchAuthType.LoginID == null &&
                                 merchAuthType.TransactionKey == null
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

        Mock<ITransactionController> CreateMockTransactionController(
            ITransactionRequest txnReq,
            ICreditCard creditCard)
        {
            var response = TransactionResponseType.DECLINED;

            if (creditCard.ExpiryDate.CompareTo(DateTime.Today) > 0 &&
                txnReq.Amount > 0.0M)
            {
                response = TransactionResponseType.OK;
            }
            // We use a mock here rather than linq to moq because we need to setup execute for a strict mock behavior.
            var mockTxnCtrl = new Mock<ITransactionController>(MockBehavior.Strict);
            var seq = new MockSequence();

            mockTxnCtrl.InSequence(seq).Setup(ex => ex.Execute());
            mockTxnCtrl.InSequence(seq).Setup(ar => ar.GetApiResponse()).Returns(response);
            mockTxnCtrl.InSequence(seq).SetupGet(r => r.TransactionRequest).Returns(txnReq);

            return mockTxnCtrl;
        }

        // [Test]
        // public void EnvironmentSetCorrectly_WhenFacadeInitializesPaymentGatewayInterface()
        // {
        //     // Arrange
        //     var env = CreateMockEnvironment();
        //     var merchAuthType = CreateMockMerchantAuthenticationType();
        //     var billingAddress = CreateMockBillingAddress();
        //     var creditCard = CreateMockCreditCard(DateTime.Today.AddDays(1));
        //     var txnReq = CreateMockTransactionRequest(billingAddress, creditCard);
        //     var txnCtrl = CreateMockTransactionController(txnReq,creditCard);
        //     var paymentProcessor = new PaymentProcessor(env, merchAuthType, txnCtrl.Object);

        //     // Act
        //     paymentProcessor.InitializePaymentGatewayInterface();

        //     // Assert
        //     env.environmentVariableTarget.Should().NotBe(EnvironmentTarget.UNINITIALIZED);
        // }

        // [Test]
        // public void MerchantAuthenticated_WhenFacadeInitializesPaymentGatewayInterface()
        // {
        //     // Arrange
        //     var env = CreateMockEnvironment();
        //     var merchAuthType = CreateMockMerchantAuthenticationType();
        //     var billingAddress = CreateMockBillingAddress();
        //     var creditCard = CreateMockCreditCard(DateTime.Today.AddDays(1));
        //     var txnReq = CreateMockTransactionRequest(billingAddress, creditCard);
        //     var txnCtrl = CreateMockTransactionController(txnReq,creditCard);
        //     var paymentProcessor = new PaymentProcessor(env, merchAuthType, txnCtrl.Object);

        //     // Act
        //     paymentProcessor.InitializePaymentGatewayInterface();

        //     // Assert
        //     merchAuthType.LoginID.Should().NotBe(null);
        //     merchAuthType.TransactionKey.Should().NotBe(null);
        // }


        // [Test]
        // public void PaymentSubmittedSuccesfully_WhenFacadeInitializesCorrectlyAndChecksCreditCardExpiry()
        // {
        //     // Arrange
        //     var env = CreateMockEnvironment();
        //     var merchAuthType = CreateMockMerchantAuthenticationType();
        //     var billingAddress = CreateMockBillingAddress();
        //     var creditCard = CreateMockCreditCard(DateTime.Today.AddDays(1));
        //     var txnReq = CreateMockTransactionRequest(billingAddress, creditCard);
        //     var txnCtrl = CreateMockTransactionController(txnReq,creditCard);
        //     var paymentProcessor = new PaymentProcessor(env, merchAuthType, txnCtrl.Object);

        //     // Act
        //     paymentProcessor.InitializePaymentGatewayInterface();

        //     // Assert
        //     paymentProcessor.SubmitPayment().Should().BeTrue();


        //     txnCtrl.Verify(tc => tc.Execute(), Times.Once);
        //     txnCtrl.Verify(ar => ar.GetApiResponse(), Times.Once);
        // }

        // [Test]
        // public void PaymentSubmissionFails_WhenCreditCardIsExpired()
        // {
        //     // Arrange
        //     var env = CreateMockEnvironment();
        //     var merchAuthType = CreateMockMerchantAuthenticationType();
        //     var billingAddress = CreateMockBillingAddress();
        //     var creditCard = CreateMockCreditCard(DateTime.Today.AddDays(-1));
        //     var txnReq = CreateMockTransactionRequest(billingAddress, creditCard);
        //     var txnCtrl = CreateMockTransactionController(txnReq,creditCard);
        //     var paymentProcessor = new PaymentProcessor(env, merchAuthType, txnCtrl.Object);

        //     // Act
        //     paymentProcessor.InitializePaymentGatewayInterface();

        //     // Assert
        //     paymentProcessor.SubmitPayment().Should().BeFalse();
        // }

    }
}
