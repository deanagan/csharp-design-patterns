using System;
using Xunit;
using Moq;
using FluentAssertions;

namespace Adapter.Tests
{
    public class AdapterShould
    {
        [Fact]
        public void Return_SocialMediaProfileContent_WhenAccessingViaAdapter()
        {
            // Arrange
            var mockSocialMediaProfile = new Mock<ISocialMediaProfile>();
            mockSocialMediaProfile.SetupGet(msmp => msmp.Name).Returns("John Smith");
            mockSocialMediaProfile.SetupGet(msmp => msmp.UserName).Returns("jsmith");
            mockSocialMediaProfile.SetupGet(msmp => msmp.Email).Returns("jsmith@google.com");

            // Act
            var goodReadsProfile = new SocialMediaProfileAdapter(mockSocialMediaProfile.Object);

            // Assert
            goodReadsProfile.Name.Should().Be(mockSocialMediaProfile.Object.Name);
            goodReadsProfile.Email.Should().Be(mockSocialMediaProfile.Object.Email);
        }


        [Fact]
        public void ThrowException_WhenSocialMediaAdapteeIsNull()
        {
            // Act
            Action act = () => new SocialMediaProfileAdapter(null);

            // Assert
            act.Should().ThrowExactly<ArgumentNullException>();
        }
    }
}
