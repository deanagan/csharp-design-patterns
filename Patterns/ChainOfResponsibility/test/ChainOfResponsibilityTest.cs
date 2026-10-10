using ChainOfResponsibility;
using NSubstitute;
using Shouldly;
using Xunit;
using System.Collections.Generic;

namespace ChainOfResponsibilityTest;

public static class GatewayExtension
{
    public static void IsCalledWith(
        this IPaymentGateway gateway,
        ICreditCard creditCard,
        int callCount,
        ICreditCardHandler creditCardHandler)
    {
        gateway.Received(callCount).SubmitVerification(
            Arg.Is<ICreditCardHandler>(cch => cch.GetType() == creditCardHandler.GetType()), 
            creditCard
        );
    }
}

public class ChainOfResponsibilityShould
{
    private readonly ICreditCardHandler _creditCardHandler;
    private readonly IPaymentGateway _paymentGateway;

    private readonly List<string> validStartingCardNumbers = ["3", "4", "5"];

    public ChainOfResponsibilityShould()
    {
        _paymentGateway = Substitute.For<IPaymentGateway>();
        _creditCardHandler = new VisaCardHandler(_paymentGateway);
        _creditCardHandler.SetNext(new AmexCardHandler(_paymentGateway))
                            .SetNext(new MastercardHandler(_paymentGateway));
    }

    private ICreditCard PrepareCreditCardPayment(string cardNumber)
    {
        // 1. Configure the payment gateway behavior (no Mock.Get() needed)
        _paymentGateway
            .SubmitVerification(Arg.Any<ICreditCardHandler>(), Arg.Any<ICreditCard>())
            .Returns(validStartingCardNumbers.Contains(cardNumber[..1]));

        // 2. Create and stub the credit card substitute inline
        var creditCard = Substitute.For<ICreditCard>();
        creditCard.Number.Returns(cardNumber);
        return creditCard;
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
        isValid.ShouldSatisfyAllConditions(
            "credit card",
            () => isValid.ShouldBeTrue(),
            () => _paymentGateway.IsCalledWith(creditCard, 1, expectedCreditCardHandler)
        );
    }

    [Fact]
    public void NotSubmitPayments_WhenInvalidCardSubmittedForVerification()
    {
        // Arrange
        var invalidCreditCard = PrepareCreditCardPayment("9244683");

        // Act
        var isValid = _creditCardHandler.IsCreditCardValid(invalidCreditCard);

        // Assert
        isValid.ShouldSatisfyAllConditions(
            "credit card",
            () => isValid.ShouldBeFalse(),
            () => _paymentGateway.DidNotReceive().SubmitVerification(
                Arg.Any<ICreditCardHandler>(),
                Arg.Any<ICreditCard>())
        );
    }
}
