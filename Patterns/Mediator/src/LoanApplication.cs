namespace Mediator;

public sealed record LoanApplication(
    string ApplicationId,
    string BorrowerName,
    decimal RequestedAmount,
    decimal AnnualIncome,
    int CreditScore,
    decimal MonthlyPayment,
    bool IsSecured = false,
    decimal? CollateralValue = null);
