using Moq;
using Xunit;
using FluentAssertions;
using AutoFixture;

namespace Decorator.Test
{
    public class DecoratorShould
    {
        private Fixture _fixture = new Fixture();
        private IHtmlElement _htmlElement;
        private string _DummyElement;
        private string _DummyLink;

        public DecoratorShould()
        {
            _htmlElement = Mock.Of<IHtmlElement>();
            _DummyElement = _fixture.Create<string>();
            _DummyLink = "www." + _fixture.Create<string>() + ".com";
            Mock.Get(_htmlElement).Setup(htmlElement => htmlElement.GetHtmlElement()).Returns(_DummyElement);            
        }

        [Fact]
        public void BoldenHtmlElement_WhenUsingBoldenDecorator()
        {
            // Arrange
            var bolden = new BoldenHtmlElement(_htmlElement);
            // Act
            var result = bolden.GetHtmlElement();
            // Assert
            using (new FluentAssertions.Execution.AssertionScope("boldened html element"))
            {
                result.Should().Be($"<b>{_DummyElement}</b>");
                Mock.Get(_htmlElement).Verify(htmlElement => htmlElement.GetHtmlElement(), Times.Once);
            }
        }

        [Fact]
        public void ItalicizeHtmlElement_WhenUsingItalicizeDecorator()
        {
            // Arrange
            var italicized = new ItalicizeHtmlElement(_htmlElement);
            // Act
            var result = italicized.GetHtmlElement();
            // Assert
            using (new FluentAssertions.Execution.AssertionScope("italicized html element"))
            {
                result.Should().Be($"<em>{_DummyElement}</em>");
                Mock.Get(_htmlElement).Verify(htmlElement => htmlElement.GetHtmlElement(), Times.Once);
            }
        }

        [Fact]
        public void HyperlinkHtmlElement_WhenUsingHyperlinkifyDecorator()
        {
            // Arrange
            var hyperlinked = new HyperLinkifyHtmlElement(_DummyLink, _htmlElement);
            // Act
            var result = hyperlinked.GetHtmlElement();
            // Assert
            using (new FluentAssertions.Execution.AssertionScope("hyperlinked html element"))
            {
                result.Should().Be($"<a href=\"{_DummyLink}\">{_DummyElement}</a>");
                Mock.Get(_htmlElement).Verify(htmlElement => htmlElement.GetHtmlElement(), Times.Once);
            }
        }

        [Fact]
        public void HyperlinkAndBoldenHtmlElement_WhenUsingBoldenHyperlinkifyDecorator()
        {
            // Arrange
            var hyperlinked = new HyperLinkifyHtmlElement(_DummyLink, _htmlElement);
            var boldenedHyperlinked = new BoldenHtmlElement(hyperlinked);
            // Act
            var result = boldenedHyperlinked.GetHtmlElement();
            // Assert
            using (new FluentAssertions.Execution.AssertionScope("hyperlinked and boldened html element"))
            {
                result.Should().Be($"<b><a href=\"{_DummyLink}\">{_DummyElement}</a></b>");
                Mock.Get(_htmlElement).Verify(htmlElement => htmlElement.GetHtmlElement(), Times.Once);
            }
        }


        [Fact]
        public void HyperlinkItalicizeAndBoldenHtmlElement_WhenUsingHyperlinkifyItalicizeBoldenDecorator()
        {
            // Arrange
            var hyperlinked = new HyperLinkifyHtmlElement(_DummyLink, _htmlElement);
            var italicizedHyperlinked = new ItalicizeHtmlElement(hyperlinked);
            var boldenedItalicizedHyperlinked = new BoldenHtmlElement(italicizedHyperlinked);

            // Act
            var result = boldenedItalicizedHyperlinked.GetHtmlElement();
            // Assert
            using (new FluentAssertions.Execution.AssertionScope("hyperlinked and boldened html element"))
            {
                result.Should().Be($"<b><em><a href=\"{_DummyLink}\">{_DummyElement}</a></em></b>");
                Mock.Get(_htmlElement).Verify(htmlElement => htmlElement.GetHtmlElement(), Times.Once);
            }
        }
    }
}
