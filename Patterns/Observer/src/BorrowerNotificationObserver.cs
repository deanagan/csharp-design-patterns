using System;

namespace Observer;

public sealed class BorrowerNotificationObserver(
    IBorrowerStatusNotificationService notificationService) : ILoanApplicationObserver
{
    private readonly IBorrowerStatusNotificationService _notificationService =
        notificationService ?? throw new ArgumentNullException(nameof(notificationService));

    public void OnStatusChanged(LoanApplicationStatusChanged change)
    {
        ArgumentNullException.ThrowIfNull(change);

        _notificationService.NotifyStatusChanged(
            change.ApplicationId,
            change.BorrowerName,
            change.PreviousStatus,
            change.NewStatus);
    }
}
