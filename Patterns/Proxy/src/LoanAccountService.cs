using System;
using System.Collections.Generic;

namespace Proxy;

public sealed class LoanAccountService : ILoanAccountService
{
    private readonly Dictionary<string, LoanAccount> _accounts;

    public LoanAccountService(IEnumerable<LoanAccount> accounts)
    {
        ArgumentNullException.ThrowIfNull(accounts);

        _accounts = new Dictionary<string, LoanAccount>(StringComparer.Ordinal);
        foreach (var account in accounts)
        {
            ArgumentNullException.ThrowIfNull(account);
            _accounts.Add(account.LoanId, account);
        }
    }

    public LoanAccount? GetAccount(string loanId) =>
        _accounts.TryGetValue(loanId, out var account) ? account : null;

    public bool UpdateRepaymentInstructions(string loanId, string repaymentInstructions)
    {
        if (!_accounts.TryGetValue(loanId, out var account))
        {
            return false;
        }

        _accounts[loanId] = account with { RepaymentInstructions = repaymentInstructions };
        return true;
    }

    public bool InitiatePayout(string loanId, decimal amount)
    {
        if (amount <= 0m
            || !_accounts.TryGetValue(loanId, out var account)
            || amount > account.OutstandingBalance)
        {
            return false;
        }

        _accounts[loanId] = account with
        {
            OutstandingBalance = account.OutstandingBalance - amount
        };
        return true;
    }
}
