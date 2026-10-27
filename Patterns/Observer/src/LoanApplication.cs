using System;
using System.Collections.Generic;

namespace Observer;

public sealed class LoanApplication
{
    private readonly List<ILoanApplicationObserver> _observers = [];

    public LoanApplication(string applicationId, string borrowerName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(applicationId);
        ArgumentException.ThrowIfNullOrWhiteSpace(borrowerName);

        ApplicationId = applicationId;
        BorrowerName = borrowerName;
    }

    public string ApplicationId { get; }

    public string BorrowerName { get; }

    public LoanApplicationStatus Status { get; private set; } = LoanApplicationStatus.Draft;

    public void Attach(ILoanApplicationObserver observer)
    {
        ArgumentNullException.ThrowIfNull(observer);

        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
    }

    public void Detach(ILoanApplicationObserver observer)
    {
        ArgumentNullException.ThrowIfNull(observer);
        _observers.Remove(observer);
    }

    public bool ChangeStatus(LoanApplicationStatus newStatus)
    {
        if (Status == newStatus)
        {
            return false;
        }

        var previousStatus = Status;
        Status = newStatus;

        var change = new LoanApplicationStatusChanged(
            ApplicationId,
            BorrowerName,
            previousStatus,
            newStatus);

        foreach (var observer in _observers.ToArray())
        {
            observer.OnStatusChanged(change);
        }

        return true;
    }
}
