namespace Mediator;

public enum UnderwritingCheckOutcome
{
    Passed,
    Declined,
    ManualReview
}

public sealed record UnderwritingCheckResult(
    UnderwritingCheckOutcome Outcome,
    string Reason)
{
    public static UnderwritingCheckResult Passed(string reason) =>
        new(UnderwritingCheckOutcome.Passed, reason);

    public static UnderwritingCheckResult Declined(string reason) =>
        new(UnderwritingCheckOutcome.Declined, reason);

    public static UnderwritingCheckResult RequiresManualReview(string reason) =>
        new(UnderwritingCheckOutcome.ManualReview, reason);
}
