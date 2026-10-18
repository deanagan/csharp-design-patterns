namespace Facade;

public interface IFraudService
{
    FraudCheckResult ValidateRisk(string customerEmail, string cardNumber, decimal amount);
}

