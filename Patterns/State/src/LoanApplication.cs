using System;

namespace State;

public sealed class LoanApplication(
    string applicationId,
    string borrowerName,
    decimal requestedAmount)
{
    private ILoanApplicationState _state = new DraftLoanApplicationState();

    public string ApplicationId { get; } =
        string.IsNullOrWhiteSpace(applicationId)
            ? throw new ArgumentException("Application ID is required.", nameof(applicationId))
            : applicationId;

    public string BorrowerName { get; } =
        string.IsNullOrWhiteSpace(borrowerName)
            ? throw new ArgumentException("Borrower name is required.", nameof(borrowerName))
            : borrowerName;

    public decimal RequestedAmount { get; } =
        requestedAmount > 0m
            ? requestedAmount
            : throw new ArgumentOutOfRangeException(nameof(requestedAmount));

    public LoanApplicationStatus Status => _state.Status;

    public string? DeclineReason { get; private set; }

    public void Submit() => _state.Submit(this);

    public void BeginReview() => _state.BeginReview(this);

    public void Approve() => _state.Approve(this);

    public void Decline(string reason)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(reason);
        _state.Decline(this, reason);
    }

    public void Fund() => _state.Fund(this);

    public void Withdraw() => _state.Withdraw(this);

    internal void SetState(ILoanApplicationState state) => _state = state;

    internal void SetDeclineReason(string reason) => DeclineReason = reason;
}
