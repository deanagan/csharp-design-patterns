namespace Mediator;

public interface ILoanDecisionNotifier
{
    void Notify(LoanApplication application, LoanDecisionResult result);
}
