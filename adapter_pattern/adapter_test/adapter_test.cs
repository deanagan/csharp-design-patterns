using NUnit.Framework;
using Moq;
using FluentAssertions;

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
            // Arrange
            var mockSocialMediaProfile = new Mock<ISocialMediaProfile>();
            mockSocialMediaProfile.Setup(msmp => msmp.Name()).Returns("John Smith");
            mockSocialMediaProfile.Setup(msmp => msmp.UserName()).Returns("jsmith");
            mockSocialMediaProfile.Setup(msmp => msmp.Email()).Returns("jsmith@google.com");
            
            // Act
            var goodReadsProfile = new SocialMediaProfileAdapter(mockSocialMediaProfile.Object);

            // Assert
            goodReadsProfile.Name().Should().Be(mockSocialMediaProfile.Object.Name());
            goodReadsProfile.Email().Should().Be(mockSocialMediaProfile.Object.Email());
        }

        
        [Test]
        public void SocialMediaProfileAdapter_ThrowsException_WhenSocialMediaAdapteeIsNull()
        {
            Assert.Fail();
        }
    }
}