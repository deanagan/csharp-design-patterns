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
            var xmlTextBlob = new XmlTextBlobFactory(this.textDownloader);

            // Act
            var blob1 = xmlTextBlob.GetTextBlob(1);
            var blob2 = xmlTextBlob.GetTextBlob(1);

            // Assert
            blob1.GetHashCode().Should().Be(blob2.GetHashCode());
        }

        [Fact]
        public void TextDownloaderInvokedWithMatchingId_WhenXmlTextBlobDownloadInvoked()
        {
            // Arrange
            var xmlTextBlob = new XmlTextBlobFactory(this.textDownloader);

            // Act
            var blob = xmlTextBlob.GetTextBlob(1);
            blob.Download(1, "Hello");

            // Assert
            Mock.Get(textDownloader).Verify(td => td.DownloadFile($"https://localhost:5001/api/getitem/1", It.IsAny<string>()));
        }
    }
}
