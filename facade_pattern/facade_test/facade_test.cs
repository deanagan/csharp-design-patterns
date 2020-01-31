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

        ICreditCard CreateMockCreditCard(bool isExpired)
        {
            return Mock.Of<ICreditCard>
            (
                creditCard =>
                    creditCard.Type == CreditCard.VISA &&
                    creditCard.AccountNumber == "123456789" &&
                    creditCard.CVC == "987" &&
                    creditCard.ExpiryDate = new DateTime(DateTime.AddDays(isExpired ? -1 : 1))
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
        public void SubmitPaymentReturnsOK_WithoutUsingFacade()
        {
           
            
        }

        [Test]
        public void SubmitPaymentReturnsOK_WithFacadeComposedOfTransactionController()
        {
            
        }

    }
}