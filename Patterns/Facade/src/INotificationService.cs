namespace Facade;

public interface INotificationService
{
    void SendPaymentConfirmation(string customerEmail, string orderId, decimal amount, string transactionId);
    void SendPaymentFailedAlert(string customerEmail, string orderId, string reason);
}

