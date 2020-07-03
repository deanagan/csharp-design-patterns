using System.Net.Mail;

namespace Adapter
{
    public interface IGoodReadsProfile
    {
        string Name { get; }
        MailAddress Email { get; }
    }
}
