using System;

namespace Prototype;

public sealed class LoanProductTerms
{
    public LoanProductTerms(
        decimal requestedAmount,
        decimal annualInterestRate,
        int termInMonths,
        decimal originationFee)
    {
        Revise(requestedAmount, annualInterestRate, termInMonths, originationFee);
    }

    public decimal RequestedAmount { get; private set; }

    public decimal AnnualInterestRate { get; private set; }

    public int TermInMonths { get; private set; }

    public decimal OriginationFee { get; private set; }

    public void Revise(
        decimal requestedAmount,
        decimal annualInterestRate,
        int termInMonths,
        decimal originationFee)
    {
        if (requestedAmount <= 0m)
        {
            throw new ArgumentOutOfRangeException(nameof(requestedAmount));
        }

        if (annualInterestRate < 0m)
        {
            throw new ArgumentOutOfRangeException(nameof(annualInterestRate));
        }

        if (termInMonths <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(termInMonths));
        }

        if (originationFee < 0m)
        {
            throw new ArgumentOutOfRangeException(nameof(originationFee));
        }

        RequestedAmount = requestedAmount;
        AnnualInterestRate = annualInterestRate;
        TermInMonths = termInMonths;
        OriginationFee = originationFee;
    }

    public LoanProductTerms Copy() =>
        new(RequestedAmount, AnnualInterestRate, TermInMonths, OriginationFee);
}
