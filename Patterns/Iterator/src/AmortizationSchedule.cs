using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Iterator;

public sealed class AmortizationSchedule(IEnumerable<LoanInstallment> installments)
    : IEnumerable<LoanInstallment>
{
    public IReadOnlyList<LoanInstallment> Installments { get; } = Array.AsReadOnly(installments?.ToArray()
            ?? throw new ArgumentNullException(nameof(installments)));

    public decimal TotalInterestPayable =>
        Installments.Sum(installment => installment.InterestComponent);

    public decimal TotalPrincipalPayable =>
        Installments.Sum(installment => installment.PrincipalComponent);

    public LoanInstallment? GetInstallmentForMonth(int month) =>
        Installments.FirstOrDefault(installment => installment.Month == month);

    public IEnumerator<LoanInstallment> GetEnumerator() =>
        Installments.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
