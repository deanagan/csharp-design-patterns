using System;

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
        public string Name()
        {
            return _socialMediaProfile.Name();
        }

        public string Email()
        {
            return _socialMediaProfile.Email();
        }
    }
}