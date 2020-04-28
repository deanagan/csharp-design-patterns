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
            var mockSocialMediaProfile = Mock.Of<ISocialMediaProfile>(msmp => msmp.Name == "John Smith" &&
                                                                      msmp.UserName == "jsmith" &&
                                                                      msmp.Email == "jsmith@google.com");

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
