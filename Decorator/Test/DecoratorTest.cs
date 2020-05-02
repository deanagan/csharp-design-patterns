using Moq;
using Xunit;
using FluentAssertions;

namespace Decorator.Test
{
    public class DecoratorShould
    {
        private IHtmlElement _htmlElement;
        private string _Dummy = "Dummy";

        public DecoratorShould()
        {
            _htmlElement = Mock.Of<IHtmlElement>();
            Mock.Get(_htmlElement).Setup(htmlElement => htmlElement.GetHtmlElement()).Returns(_Dummy);
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
                result.Should().Be($"<b>{_Dummy}</b>");
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
                result.Should().Be($"<em>{_Dummy}</em>");
                Mock.Get(_htmlElement).Verify(htmlElement => htmlElement.GetHtmlElement(), Times.Once);
            }
        }


    }
}
