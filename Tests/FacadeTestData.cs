using System;
using System.Collections;
using System.Collections.Generic;
using Moq;
using FluentAssertions;

namespace Facade.Test
{
    public class FacadeTestData
    {

        public static IBillingAddress CreateMockBillingAddress()
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

        public static ICreditCard CreateMockCreditCard(DateTime expiryDate)
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

        public static IEnvironment CreateMockEnvironment()
        {
            return Mock.Of<IEnvironment>
            (
                environment => environment.environmentVariableTarget == EnvironmentTarget.UNINITIALIZED
            );
        }

        public static IMerchantAuthenticationType CreateMockMerchantAuthenticationType()
        {
            return Mock.Of<IMerchantAuthenticationType>
            (
                merchAuthType => merchAuthType.LoginID == null &&
                                merchAuthType.TransactionKey == null
            );
        }

        public static ITransactionRequest CreateMockTransactionRequest(
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

        public static ITransactionController CreateMockTransactionController(
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

            return mockTxnCtrl.Object;
        }
    }
}
