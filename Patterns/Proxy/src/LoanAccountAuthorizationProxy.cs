using System;

namespace Proxy;

public sealed class LoanAccountAuthorizationProxy(
    LoanUserContext user,
    ILoanAccountService realService) : ILoanAccountService
{
    private readonly LoanUserContext _user =
        user ?? throw new ArgumentNullException(nameof(user));

    private readonly ILoanAccountService _realService =
        realService ?? throw new ArgumentNullException(nameof(realService));

    public LoanAccount? GetAccount(string loanId)
    {
        var account = _realService.GetAccount(loanId);
        return account is not null && CanView(account) ? account : null;
    }

    public bool UpdateRepaymentInstructions(string loanId, string repaymentInstructions)
    {
        var account = _realService.GetAccount(loanId);
        if (account is null || !CanUpdateRepaymentInstructions(account))
        {
            return false;
        }

        return _realService.UpdateRepaymentInstructions(loanId, repaymentInstructions);
    }

    public bool InitiatePayout(string loanId, decimal amount)
    {
        if (_user.Role != LoanUserRole.ServicingAdministrator)
        {
            return false;
        }

        return _realService.InitiatePayout(loanId, amount);
    }

    private bool CanView(LoanAccount account) =>
        _user.Role is LoanUserRole.LoanOfficer or LoanUserRole.ServicingAdministrator
        || IsBorrower(account);

    private bool CanUpdateRepaymentInstructions(LoanAccount account) =>
        _user.Role == LoanUserRole.ServicingAdministrator || IsBorrower(account);

    private bool IsBorrower(LoanAccount account) =>
        _user.Role == LoanUserRole.Borrower
        && string.Equals(account.BorrowerId, _user.UserId, StringComparison.Ordinal);
}
