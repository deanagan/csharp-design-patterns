using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iterator;
using Shouldly;
using Xunit;

namespace IteratorTest;

public class AmortizationScheduleTest
{
    [Fact]
    public void CalculatesPrincipalAndInterestTotals()
    {
        var schedule = new AmortizationSchedule(
        [
            new LoanInstallment(1, 900m, 100m),
            new LoanInstallment(2, 910m, 90m)
        ]);

        schedule.TotalPrincipalPayable.ShouldBe(1_810m);
        schedule.TotalInterestPayable.ShouldBe(190m);
    }

    [Fact]
    public void ReturnsInstallmentForMonthOrNullWhenMissing()
    {
        var firstInstallment = new LoanInstallment(1, 900m, 100m);
        var schedule = new AmortizationSchedule([firstInstallment]);

        schedule.GetInstallmentForMonth(1).ShouldBeSameAs(firstInstallment);
        schedule.GetInstallmentForMonth(2).ShouldBeNull();
    }

    [Fact]
    public void SupportsGenericAndNonGenericEnumeration()
    {
        var installments = new[]
        {
            new LoanInstallment(1, 900m, 100m),
            new LoanInstallment(2, 910m, 90m)
        };
        var schedule = new AmortizationSchedule(installments);

        schedule.ShouldBe(installments);
        ((IEnumerable)schedule).Cast<LoanInstallment>().ShouldBe(installments);
    }

    [Fact]
    public void CopiesAndExposesInstallmentsAsReadOnly()
    {
        var installments = new List<LoanInstallment>
        {
            new(1, 900m, 100m)
        };
        var schedule = new AmortizationSchedule(installments);
        installments.Add(new LoanInstallment(2, 910m, 90m));

        schedule.Installments.Count.ShouldBe(1);
        Should.Throw<NotSupportedException>(() =>
            ((IList<LoanInstallment>)schedule.Installments).Add(new LoanInstallment(2, 910m, 90m)));
    }

    [Fact]
    public void RejectsNullInstallmentCollection()
    {
        Should.Throw<ArgumentNullException>(() => new AmortizationSchedule(null!));
    }
}
