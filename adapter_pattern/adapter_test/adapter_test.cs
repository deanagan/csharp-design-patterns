using NUnit.Framework;

namespace Adapter.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GoodReadsProfileReturned_Matches_SocialMediaProfile_WhenRegisteringViaAdapter()
        {
            Assert.Fail();
        }

        
        [Test]
        public void SocialMediaProfileAdapter_ThrowsException_WhenSocialMediaAdapteeIsNull()
        {
            Assert.Fail();
        }
    }
}