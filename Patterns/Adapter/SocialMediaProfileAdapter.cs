using System;
using System.Net.Mail;

namespace Adapter
{
    public class SocialMediaProfileAdapter : IGoodReadsProfile
    {
        private ISocialMediaProfile _socialMediaProfile;
        public SocialMediaProfileAdapter(ISocialMediaProfile socialMediaProfile)
        {
            if (socialMediaProfile == null)
            {
                throw new ArgumentNullException();
            }
            _socialMediaProfile = socialMediaProfile;
        }

        public string Name { get { return _socialMediaProfile.Name; } }

        public MailAddress Email { get { return _socialMediaProfile.Email; } }
    }
}
