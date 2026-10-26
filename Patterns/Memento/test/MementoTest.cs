using System;
using Memento;
using Shouldly;
using Xunit;

namespace MementoTest;

public class MementoTest
{
    [Fact]
    public void RestoresTheSavedLoanTermsAfterARevision()
    {
        var scenario = CreateScenario();
        var history = new LoanScenarioHistory();
        history.Save(scenario.Save());

        scenario.ReviseTerms(80_000m, 0.0725m, 48);
        scenario.Restore(history.GetLatest());

        scenario.RequestedAmount.ShouldBe(100_000m);
        scenario.AnnualInterestRate.ShouldBe(0.065m);
        scenario.TermInMonths.ShouldBe(60);
    }

    [Fact]
    public void RestoresTheSelectedScenarioFromMultipleSnapshots()
    {
        var scenario = CreateScenario();
        var history = new LoanScenarioHistory();
        history.Save(scenario.Save());

        scenario.ReviseTerms(80_000m, 0.0725m, 48);
        history.Save(scenario.Save());

        scenario.ReviseTerms(60_000m, 0.08m, 36);
        scenario.Restore(history.Get(1));

        scenario.RequestedAmount.ShouldBe(80_000m);
        scenario.AnnualInterestRate.ShouldBe(0.0725m);
        scenario.TermInMonths.ShouldBe(48);
    }

    [Fact]
    public void KeepsSnapshotsIndependentFromLaterScenarioChanges()
    {
        var scenario = CreateScenario();
        var snapshot = scenario.Save();

        scenario.ReviseTerms(50_000m, 0.09m, 24);
        scenario.Restore(snapshot);

        scenario.RequestedAmount.ShouldBe(100_000m);
        scenario.AnnualInterestRate.ShouldBe(0.065m);
        scenario.TermInMonths.ShouldBe(60);
    }

    [Fact]
    public void DoesNotRestoreSnapshotFromAnotherLoanApplication()
    {
        var scenario = CreateScenario();
        var otherScenario = new LoanApplicationScenario("LN-2002", 25_000m, 0.08m, 24);

        Should.Throw<ArgumentException>(() => scenario.Restore(otherScenario.Save()));
    }

    [Fact]
    public void ThrowsWhenNoScenarioHasBeenSaved()
    {
        var history = new LoanScenarioHistory();

        Should.Throw<InvalidOperationException>(() => history.GetLatest());
    }

    [Fact]
    public void RejectsInvalidLoanTerms()
    {
        Should.Throw<ArgumentOutOfRangeException>(() =>
            new LoanApplicationScenario("LN-1001", 0m, 0.065m, 60));
    }

    private static LoanApplicationScenario CreateScenario() =>
        new("LN-1001", 100_000m, 0.065m, 60);
}
