using System;
using Xunit;
using Moq;
using FluentAssertions;
using AutoFixture;
using System.Net.Mail;

namespace Adapter.Tests
{
    public class AdapterShould
    {
        private Fixture _fixture = new Fixture();

        [Fact]
        public void Return_SocialMediaProfileContent_WhenAccessingViaAdapter()
        {
            // Arrange
            var mockSocialMediaProfile = Mock.Of<ISocialMediaProfile>(msmp => msmp.Name == _fixture.Create<string>() &&
                                                                      msmp.UserName == _fixture.Create<string>() &&
                                                                      msmp.Email == _fixture.Create<MailAddress>());

            // Act
            var goodReadsProfile = new SocialMediaProfileAdapter(mockSocialMediaProfile);

            // Assert
            using (new FluentAssertions.Execution.AssertionScope("profile"))
            {
                goodReadsProfile.Name.Should().Be(mockSocialMediaProfile.Name);
                goodReadsProfile.Email.Should().Be(mockSocialMediaProfile.Email);
            }

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
