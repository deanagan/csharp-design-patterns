using System;
using System.Collections.Generic;
using System.Linq;

namespace Mediator;

public sealed class LoanUnderwritingMediator
{
    private readonly IReadOnlyList<IUnderwritingCheck> _standardChecks;
    private readonly IUnderwritingCheck _collateralCheck;
    private readonly ILoanDecisionNotifier _notifier;

    public LoanUnderwritingMediator(
        IEnumerable<IUnderwritingCheck> standardChecks,
        IUnderwritingCheck collateralCheck,
        ILoanDecisionNotifier notifier)
    {
        ArgumentNullException.ThrowIfNull(standardChecks);
        ArgumentNullException.ThrowIfNull(collateralCheck);
        ArgumentNullException.ThrowIfNull(notifier);

        var checks = standardChecks.ToArray();
        if (checks.Length == 0)
        {
            throw new ArgumentException("At least one standard underwriting check is required.", nameof(standardChecks));
        }

        if (checks.Any(check => check is null))
        {
            throw new ArgumentException("Underwriting checks cannot contain null values.", nameof(standardChecks));
        }

        _standardChecks = Array.AsReadOnly(checks);
        _collateralCheck = collateralCheck;
        _notifier = notifier;
    }

    public LoanDecisionResult Evaluate(LoanApplication application)
    {
        ArgumentNullException.ThrowIfNull(application);

        var results = _standardChecks
            .Select(check => check.Evaluate(application))
            .ToList();

        if (application.IsSecured)
        {
            results.Add(_collateralCheck.Evaluate(application));
        }

        var decision = results.Any(result => result.Outcome == UnderwritingCheckOutcome.Declined)
            ? LoanDecision.Declined
            : results.Any(result => result.Outcome == UnderwritingCheckOutcome.ManualReview)
                ? LoanDecision.ManualReview
                : LoanDecision.Approved;

        var reasons = results
            .Where(result => result.Outcome != UnderwritingCheckOutcome.Passed)
            .Select(result => result.Reason);
        var decisionResult = new LoanDecisionResult(decision, reasons);

        _notifier.Notify(application, decisionResult);

        return decisionResult;
    }
}
