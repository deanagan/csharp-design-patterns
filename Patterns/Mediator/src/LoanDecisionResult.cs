using System;
using System.Collections.Generic;
using System.Linq;

namespace Mediator;

public enum LoanDecision
{
    Approved,
    Declined,
    ManualReview
}

public sealed class LoanDecisionResult
{
    public LoanDecisionResult(LoanDecision decision, IEnumerable<string> reasons)
    {
        ArgumentNullException.ThrowIfNull(reasons);

        Decision = decision;
        Reasons = Array.AsReadOnly(reasons.ToArray());
    }

    public LoanDecision Decision { get; }

    public IReadOnlyList<string> Reasons { get; }
}
