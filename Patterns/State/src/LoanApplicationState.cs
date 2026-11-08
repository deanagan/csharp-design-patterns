using System;

namespace State;

internal abstract class LoanApplicationState : ILoanApplicationState
{
    public abstract LoanApplicationStatus Status { get; }

    public virtual void Submit(LoanApplication application) => ThrowInvalidAction();

    public virtual void BeginReview(LoanApplication application) => ThrowInvalidAction();

    public virtual void Approve(LoanApplication application) => ThrowInvalidAction();

    public virtual void Decline(LoanApplication application, string reason) => ThrowInvalidAction();

    public virtual void Fund(LoanApplication application) => ThrowInvalidAction();

    public virtual void Withdraw(LoanApplication application) => ThrowInvalidAction();

    private void ThrowInvalidAction() =>
        throw new InvalidOperationException($"This action is not valid while the application is {Status}.");
}
