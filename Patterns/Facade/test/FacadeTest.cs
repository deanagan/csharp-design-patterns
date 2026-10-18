using System;
using Facade;
using NSubstitute;
using Shouldly;
using Xunit;

namespace FacadeTest;

public class FacadeShould
{
    private readonly IFraudService _fraudService = Substitute.For<IFraudService>();
    private readonly IPaymentGateway _paymentGateway = Substitute.For<IPaymentGateway>();
    private readonly ILedgerService _ledgerService = Substitute.For<ILedgerService>();
    private readonly INotificationService _notificationService = Substitute.For<INotificationService>();
    private readonly IPaymentSettlementFacade _facade;

    public FacadeShould()
    {
        _facade = new PaymentSettlementFacade(
            _fraudService,
            _paymentGateway,
            _ledgerService,
            _notificationService);
    }

    private static PaymentRequest CreateSampleRequest(decimal amount = 150.00m)
    {
        return new PaymentRequest(
            OrderId: "ORD-9876",
            CustomerEmail: "customer@example.com",
            Amount: amount,
            Currency: "USD",
            CardNumber: "4111222233334444",
            CardExpiry: "12/28",
            CardCvc: "123");
    }

    [Fact]
    public void ProcessPayment_ShouldSucceed_WhenAllSubsystemsPass()
    {
        // Arrange
        var request = CreateSampleRequest();

        _fraudService.ValidateRisk(request.CustomerEmail, request.CardNumber, request.Amount)
            .Returns(new FraudCheckResult(IsPassed: true, RiskScore: 12));

        _paymentGateway.AuthorizeAndCapture(request.CardNumber, request.CardExpiry, request.CardCvc, request.Amount, request.Currency)
            .Returns(new GatewayAuthorization(IsAuthorized: true, AuthorizationCode: "AUTH-88899", GatewayMessage: "Approved"));

        _ledgerService.RecordTransaction(request.OrderId, request.Amount, request.Currency, "AUTH-88899")
            .Returns("GL-ENTRY-0042");

        // Act
        var result = _facade.ProcessPayment(request);

        // Assert
        result.ShouldNotBeNull();
        result.IsSuccessful.ShouldBeTrue();
        result.TransactionId.ShouldBe("AUTH-88899");
        result.LedgerReference.ShouldBe("GL-ENTRY-0042");
        result.FailureReason.ShouldBeNull();

        // Verify subsystem orchestration
        _fraudService.Received(1).ValidateRisk(request.CustomerEmail, request.CardNumber, request.Amount);
        _paymentGateway.Received(1).AuthorizeAndCapture(request.CardNumber, request.CardExpiry, request.CardCvc, request.Amount, request.Currency);
        _ledgerService.Received(1).RecordTransaction(request.OrderId, request.Amount, request.Currency, "AUTH-88899");
        _notificationService.Received(1).SendPaymentConfirmation(request.CustomerEmail, request.OrderId, request.Amount, "AUTH-88899");
        _notificationService.DidNotReceive().SendPaymentFailedAlert(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
    }

    [Fact]
    public void ProcessPayment_ShouldAbortAndAlert_WhenFraudCheckFails()
    {
        // Arrange
        var request = CreateSampleRequest();

        _fraudService.ValidateRisk(request.CustomerEmail, request.CardNumber, request.Amount)
            .Returns(new FraudCheckResult(IsPassed: false, RiskScore: 92, Reason: "High velocity card use detected."));

        // Act
        var result = _facade.ProcessPayment(request);

        // Assert
        result.ShouldNotBeNull();
        result.IsSuccessful.ShouldBeFalse();
        result.FailureReason.ShouldNotBeNull();
        result.FailureReason.ShouldContain("Fraud check failed");
        result.TransactionId.ShouldBeNull();
        result.LedgerReference.ShouldBeNull();

        // Verify early abort: gateway and ledger are NOT touched
        _paymentGateway.DidNotReceive().AuthorizeAndCapture(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<decimal>(), Arg.Any<string>());
        _ledgerService.DidNotReceive().RecordTransaction(Arg.Any<string>(), Arg.Any<decimal>(), Arg.Any<string>(), Arg.Any<string>());
        _notificationService.Received(1).SendPaymentFailedAlert(request.CustomerEmail, request.OrderId, "High velocity card use detected.");
        _notificationService.DidNotReceive().SendPaymentConfirmation(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<decimal>(), Arg.Any<string>());
    }

    [Fact]
    public void ProcessPayment_ShouldDeclineAndAlert_WhenGatewayDeclines()
    {
        // Arrange
        var request = CreateSampleRequest();

        _fraudService.ValidateRisk(request.CustomerEmail, request.CardNumber, request.Amount)
            .Returns(new FraudCheckResult(IsPassed: true, RiskScore: 15));

        _paymentGateway.AuthorizeAndCapture(request.CardNumber, request.CardExpiry, request.CardCvc, request.Amount, request.Currency)
            .Returns(new GatewayAuthorization(IsAuthorized: false, AuthorizationCode: null, GatewayMessage: "51: Insufficient funds"));

        // Act
        var result = _facade.ProcessPayment(request);

        // Assert
        result.ShouldNotBeNull();
        result.IsSuccessful.ShouldBeFalse();
        result.FailureReason.ShouldNotBeNull();
        result.FailureReason.ShouldContain("51: Insufficient funds");
        result.TransactionId.ShouldBeNull();
        result.LedgerReference.ShouldBeNull();

        // Ledger is NOT recorded on gateway decline
        _ledgerService.DidNotReceive().RecordTransaction(Arg.Any<string>(), Arg.Any<decimal>(), Arg.Any<string>(), Arg.Any<string>());
        _notificationService.Received(1).SendPaymentFailedAlert(request.CustomerEmail, request.OrderId, "51: Insufficient funds");
        _notificationService.DidNotReceive().SendPaymentConfirmation(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<decimal>(), Arg.Any<string>());
    }

    [Fact]
    public void ProcessPayment_ShouldRejectEarly_WhenAmountIsZeroOrNegative()
    {
        // Arrange
        var invalidRequest = CreateSampleRequest(amount: 0.00m);

        // Act
        var result = _facade.ProcessPayment(invalidRequest);

        // Assert
        result.ShouldNotBeNull();
        result.IsSuccessful.ShouldBeFalse();
        result.FailureReason.ShouldNotBeNull();
        result.FailureReason.ShouldContain("greater than zero");

        // Subsystems should not be called
        _fraudService.DidNotReceive().ValidateRisk(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<decimal>());
        _paymentGateway.DidNotReceive().AuthorizeAndCapture(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<decimal>(), Arg.Any<string>());
    }

    [Fact]
    public void ProcessPayment_ShouldThrowArgumentNullException_WhenRequestIsNull()
    {
        // Act & Assert
        Should.Throw<ArgumentNullException>(() => _facade.ProcessPayment(null!));
    }
}
