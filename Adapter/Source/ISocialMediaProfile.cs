using System;

namespace Adapter
{
    public interface ISocialMediaProfile
    {
        string Name { get; }
        string UserName { get; }
        string Email { get; }
    }
}
