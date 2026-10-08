using System;

namespace Builder;

public class LoanApplication
{
    public required string ApplicationId { get; init; }
    public required LoanType ProductType { get; init; }
    public required Borrower Borrower { get; init; }
    public decimal PrincipalAmount { get; init; }
    public int TermInMonths { get; init; }
    public decimal InterestRate { get; init; }
    public decimal OriginationFee { get; init; }
    public RepaymentFrequency Frequency { get; init; } = RepaymentFrequency.Monthly;
    public Collateral? Collateral { get; init; }
    public string? BusinessRegistrationNumber { get; init; }
    public bool RequiresGuarantor { get; init; }

    public decimal TotalInterestPayable =>
        Math.Round(PrincipalAmount * InterestRate * (TermInMonths / 12.0m), 2);

    public decimal TotalRepaymentAmount =>
        PrincipalAmount + TotalInterestPayable + OriginationFee;

    public decimal MonthlyPayment =>
        TermInMonths > 0 ? Math.Round(TotalRepaymentAmount / TermInMonths, 2) : 0m;
}

