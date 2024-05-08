using System;
using System.Net.Mail;

namespace Adapter
{
    public class SocialMediaProfileAdapter : IGoodReadsProfile
    {
        private readonly ISocialMediaProfile _socialMediaProfile;
        public SocialMediaProfileAdapter(ISocialMediaProfile? socialMediaProfile)
        {
            ArgumentNullException.ThrowIfNull(socialMediaProfile);
            _socialMediaProfile = socialMediaProfile;
        }

        public string Name { get { return _socialMediaProfile.Name; } }

        public MailAddress Email { get { return _socialMediaProfile.Email; } }
    }
}
