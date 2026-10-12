namespace Composite;

public interface ICreditFacilityComponent
{
    string Name { get; }
    decimal GetTotalPrincipalBalance();
    decimal GetTotalAccruedInterest();
    decimal GetTotalAvailableCreditLimit();
}