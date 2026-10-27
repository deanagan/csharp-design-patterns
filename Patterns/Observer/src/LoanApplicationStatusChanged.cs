namespace Observer;

public sealed record LoanApplicationStatusChanged(
    string ApplicationId,
    string BorrowerName,
    LoanApplicationStatus PreviousStatus,
    LoanApplicationStatus NewStatus);
