using System;

namespace Memento;

public sealed class LoanApplicationScenario
{
    public LoanApplicationScenario(
        string applicationId,
        decimal requestedAmount,
        decimal annualInterestRate,
        int termInMonths)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(applicationId);
        ValidateTerms(requestedAmount, annualInterestRate, termInMonths);

        ApplicationId = applicationId;
        RequestedAmount = requestedAmount;
        AnnualInterestRate = annualInterestRate;
        TermInMonths = termInMonths;
    }

    public string ApplicationId { get; }

    public decimal RequestedAmount { get; private set; }

    public decimal AnnualInterestRate { get; private set; }

    public int TermInMonths { get; private set; }

    public void ReviseTerms(
        decimal requestedAmount,
        decimal annualInterestRate,
        int termInMonths)
    {
        ValidateTerms(requestedAmount, annualInterestRate, termInMonths);

        RequestedAmount = requestedAmount;
        AnnualInterestRate = annualInterestRate;
        TermInMonths = termInMonths;
    }

    public LoanScenarioMemento Save() =>
        new(ApplicationId, RequestedAmount, AnnualInterestRate, TermInMonths);

    public void Restore(LoanScenarioMemento memento)
    {
        ArgumentNullException.ThrowIfNull(memento);
        if (!string.Equals(ApplicationId, memento.ApplicationId, StringComparison.Ordinal))
        {
            throw new ArgumentException(
                "The scenario snapshot belongs to a different loan application.",
                nameof(memento));
        }

        RequestedAmount = memento.RequestedAmount;
        AnnualInterestRate = memento.AnnualInterestRate;
        TermInMonths = memento.TermInMonths;
    }

    private static void ValidateTerms(
        decimal requestedAmount,
        decimal annualInterestRate,
        int termInMonths)
    {
        if (requestedAmount <= 0m)
        {
            throw new ArgumentOutOfRangeException(nameof(requestedAmount), "Amount must be greater than zero.");
        }

        if (annualInterestRate < 0m)
        {
            throw new ArgumentOutOfRangeException(nameof(annualInterestRate), "Rate cannot be negative.");
        }

        if (termInMonths <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(termInMonths), "Term must be greater than zero.");
        }
    }
}
