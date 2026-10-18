using System;

namespace Facade;

public class PaymentSettlementFacade(
    IFraudService fraudService,
    IPaymentGateway paymentGateway,
    ILedgerService ledgerService,
    INotificationService notificationService) : IPaymentSettlementFacade
{
    public PaymentResult ProcessPayment(PaymentRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.Amount <= 0)
        {
            return new PaymentResult(false, null, "Payment amount must be greater than zero.", null);
        }

        // 1. Fraud & Risk Screening Subsystem
        var fraudCheck = fraudService.ValidateRisk(request.CustomerEmail, request.CardNumber, request.Amount);
        if (!fraudCheck.IsPassed)
        {
            var reason = fraudCheck.Reason ?? "High risk score detected.";
            notificationService.SendPaymentFailedAlert(request.CustomerEmail, request.OrderId, reason);
            return new PaymentResult(false, null, $"Fraud check failed: {reason}", null);
        }

        // 2. Card Network Gateway Authorization Subsystem
        var auth = paymentGateway.AuthorizeAndCapture(
            request.CardNumber,
            request.CardExpiry,
            request.CardCvc,
            request.Amount,
            request.Currency);

        if (!auth.IsAuthorized)
        {
            var reason = auth.GatewayMessage ?? "Declined by card issuer.";
            notificationService.SendPaymentFailedAlert(request.CustomerEmail, request.OrderId, reason);
            return new PaymentResult(false, null, $"Payment declined: {reason}", null);
        }

        // 3. Accounting & Double-Entry Ledger Subsystem
        var ledgerRef = ledgerService.RecordTransaction(
            request.OrderId,
            request.Amount,
            request.Currency,
            auth.AuthorizationCode!);

        // 4. Customer Receipt Notification Subsystem
        notificationService.SendPaymentConfirmation(
            request.CustomerEmail,
            request.OrderId,
            request.Amount,
            auth.AuthorizationCode!);

        return new PaymentResult(true, auth.AuthorizationCode, null, ledgerRef);
    }
}

