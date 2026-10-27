namespace Observer;

public interface IBorrowerStatusNotificationService
{
    void NotifyStatusChanged(
        string applicationId,
        string borrowerName,
        LoanApplicationStatus previousStatus,
        LoanApplicationStatus newStatus);
}
