namespace Flyweight;

public class ActiveLoanAccount
{
    private readonly ILoanProductFlyweight _productTemplate; // Reference to shared flyweight (Intrinsic)

    // Extrinsic state (Unique to this specific loan instance)
    public string LoanId { get; }
    public string BorrowerName { get; }
    public decimal PrincipalBalance { get; }
    public decimal CustomRateDiscount { get; }

    public ActiveLoanAccount(
        ILoanProductFlyweight productTemplate,
        string loanId,
        string borrowerName,
        decimal principalBalance,
        decimal customRateDiscount)
    {
        _productTemplate = productTemplate;
        LoanId = loanId;
        BorrowerName = borrowerName;
        PrincipalBalance = principalBalance;
        CustomRateDiscount = customRateDiscount;
    }

    public decimal GetEffectiveAnnualInterest()
    {
        // Delegates base calculation to flyweight, passes extrinsic discount context
        var baseInterest = _productTemplate.CalculateBaseInterest(PrincipalBalance);
        return Math.Max(0, baseInterest - (PrincipalBalance * CustomRateDiscount));
    }

    public string GetDisclosureStatement() =>
        $"Loan [{LoanId}] for {BorrowerName}: {_productTemplate.GetRegulatoryDisclosure()}";
}

