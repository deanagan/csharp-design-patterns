using System;
using Xunit;
using Moq;
using FluentAssertions;


namespace FlyweightPattern.Test
{
    public class FlyweightPatternTest
    {
        private TextDownloader textDownloader;
        public FlyweightPatternTest()
        {
            this.textDownloader = Mock.Of<TextDownloader>();
        }
        [Fact]
        public void ObjectNotRecreated_WhenAskingSameObject()
        {
            // Arrange
            var sut = new XmlTextBlobFactory(this.textDownloader);

            // Act
            var blob1 = sut.GetTextBlob(1);
            var blob2 = sut.GetTextBlob(1);

            // Assert
            blob1.GetHashCode().Should().Be(blob2.GetHashCode());
        }
    }
}
