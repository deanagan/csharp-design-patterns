namespace Composite
{
    public class CreditFacilityLeaf : ICreditFacilityComponent
    {
        public string Name { get; }
        public decimal CreditLimit { get; }
        public decimal PrincipalBalance { get; }
        public decimal AccruedInterest { get; }

        public CreditFacilityLeaf(string name, decimal creditLimit, decimal principalBalance, decimal accruedInterest)
        {
            Name = name;
            CreditLimit = creditLimit;
            PrincipalBalance = principalBalance;
            AccruedInterest = accruedInterest;
        }

        public decimal GetTotalPrincipalBalance() => PrincipalBalance;

        public decimal GetTotalAccruedInterest() => AccruedInterest;

        public decimal GetTotalAvailableCreditLimit() => CreditLimit - PrincipalBalance;
    }
}