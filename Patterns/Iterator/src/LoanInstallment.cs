namespace Iterator;

public sealed record LoanInstallment(
    int Month,
    decimal PrincipalComponent,
    decimal InterestComponent);
