namespace Composite;

public class CreditFacilityLeaf(
    string name,
    decimal creditLimit,
    decimal principalBalance,
    decimal accruedInterest) : ICreditFacilityComponent
{
    public string Name { get; } = name;
    public decimal CreditLimit { get; } = creditLimit;
    public decimal PrincipalBalance { get; } = principalBalance;
    public decimal AccruedInterest { get; } = accruedInterest;

    public decimal GetTotalPrincipalBalance() => PrincipalBalance;

    public decimal GetTotalAccruedInterest() => AccruedInterest;

    public decimal GetTotalAvailableCreditLimit() => CreditLimit - PrincipalBalance;
}