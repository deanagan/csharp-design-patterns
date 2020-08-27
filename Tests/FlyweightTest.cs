using System;
using Xunit;
using Moq;
using FluentAssertions;

namespace Flyweight.Test
{
    public class FlyweightShould
    {
        private TextDownloader _textDownloader;
        public FlyweightShould()
        {
            this._textDownloader = Mock.Of<TextDownloader>();
        }
        [Fact]
        public void NotRecreateObject_WhenObjectIsSame()
        {
            // Arrange
            var xmlTextBlob = new XmlTextBlobFactory(this._textDownloader);

            // Act
            var blob1 = xmlTextBlob.GetTextBlob(1);
            var blob2 = xmlTextBlob.GetTextBlob(1);

            // Assert
            blob1.GetHashCode().Should().Be(blob2.GetHashCode());
        }

        [Fact]
        public void InvokeMatchingId_WhenXmlTextBlobDownloadInvoked()
        {
            // Arrange
            var xmlTextBlob = new XmlTextBlobFactory(this._textDownloader);
            var blob = xmlTextBlob.GetTextBlob(1);
            // Act
            blob.Download(1, "Hello");
            // Assert
            Mock.Get(_textDownloader).Verify(td => td.DownloadFile($"https://localhost:5001/api/getitem/1", It.IsAny<string>()));
        }
    }
}
