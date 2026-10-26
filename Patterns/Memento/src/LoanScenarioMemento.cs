namespace Memento;

public sealed class LoanScenarioMemento
{
    internal LoanScenarioMemento(
        string applicationId,
        decimal requestedAmount,
        decimal annualInterestRate,
        int termInMonths)
    {
        ApplicationId = applicationId;
        RequestedAmount = requestedAmount;
        AnnualInterestRate = annualInterestRate;
        TermInMonths = termInMonths;
    }

    internal string ApplicationId { get; }

    internal decimal RequestedAmount { get; }

    internal decimal AnnualInterestRate { get; }

    internal int TermInMonths { get; }
}
