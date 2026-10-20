namespace Flyweight;

public class ActiveLoanAccount(
    ILoanProductFlyweight productTemplate,
    string loanId,
    string borrowerName,
    decimal principalBalance,
    decimal customRateDiscount)
{
    // Reference to shared flyweight (Intrinsic)
    private readonly ILoanProductFlyweight _productTemplate = productTemplate.ThrowIfNull();

    // Extrinsic state (Unique to this specific loan instance)
    public string LoanId { get; } = loanId;
    public string BorrowerName { get; } = borrowerName;
    public decimal PrincipalBalance { get; } = principalBalance;
    public decimal CustomRateDiscount { get; } = customRateDiscount;

    public decimal GetEffectiveAnnualInterest()
    {
        // Delegates base calculation to flyweight, passes extrinsic discount context
        var baseInterest = _productTemplate.CalculateBaseInterest(PrincipalBalance);
        return Math.Max(0, baseInterest - (PrincipalBalance * CustomRateDiscount));
    }

    public string GetDisclosureStatement() =>
        $"Loan [{LoanId}] for {BorrowerName}: {_productTemplate.GetRegulatoryDisclosure()}";
}

