using System;
using System.Net.Mail;

namespace Adapter
{
    public interface ISocialMediaProfile
    {
        string Name { get; }
        string UserName { get; }
        MailAddress Email { get; }
    }
}
