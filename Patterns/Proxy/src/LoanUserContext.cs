using System;

namespace Proxy;

public sealed record LoanUserContext
{
    public LoanUserContext(string userId, LoanUserRole role)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(userId);

        UserId = userId;
        Role = role;
    }

    public string UserId { get; }

    public LoanUserRole Role { get; }
}
