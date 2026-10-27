namespace Observer;

public interface ILoanApplicationObserver
{
    void OnStatusChanged(LoanApplicationStatusChanged change);
}
