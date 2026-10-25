namespace Mediator;

public interface IUnderwritingCheck
{
    UnderwritingCheckResult Evaluate(LoanApplication application);
}
