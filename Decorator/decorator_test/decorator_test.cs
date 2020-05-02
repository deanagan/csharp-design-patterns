using Moq;
using NUnit.Framework;
using decorator_lib;

namespace decorator_test
{
    public class Tests
    {
        private Mock<IHtmlElement> _htmlElement;
        private BoldenHtmlElement _boldenHtmlElement;
        private ItalicizeHtmlElement _italicizeHtmlElement;
        private HyperLinkifyHtmlElement _hyperLinkifyHtmlElement;
        [SetUp]
        public void Setup()
        {
            _htmlElement = new Mock<IHtmlElement>();
            _boldenHtmlElement = new BoldenHtmlElement(_htmlElement.Object);
            _italicizeHtmlElement = new ItalicizeHtmlElement(_htmlElement.Object);
            _hyperLinkifyHtmlElement = new HyperLinkifyHtmlElement("www.google.com", _htmlElement.Object);
            _htmlElement.Setup(htmlElement => htmlElement.GetHtmlElement()).Returns("Google");
        }

        [Test]
        public void TestGetHtmlElementShouldReturnBoldenedElement()
        {
            Assert.That(_boldenHtmlElement.GetHtmlElement(), Is.EqualTo("<b>Google</b>"));
            _htmlElement.Verify(htmlElement => htmlElement.GetHtmlElement(), Times.Once);
        }

        [Test]
        public void TestGetHtmlElementShouldReturnItalicizedElement()
        {
            Assert.That(_italicizeHtmlElement.GetHtmlElement(), Is.EqualTo("<em>Google</em>"));
            _htmlElement.Verify(htmlElement => htmlElement.GetHtmlElement(), Times.Once);
        }

        [Test]
        public void TestGetHtmlElementShouldReturnHyperLinkifiedElement()
        {
            Assert.That(_hyperLinkifyHtmlElement.GetHtmlElement(), Is.EqualTo("<a href=\"www.google.com\">Google</a>"));
            _htmlElement.Verify(htmlElement => htmlElement.GetHtmlElement(), Times.Once);
        }

        [Test]
        public void TestGetHtmlElementShouldReturnHyperLinkifiedAndBoldenedElement()
        {
            var boldenHtmlElement = new BoldenHtmlElement(_hyperLinkifyHtmlElement);
            Assert.That(boldenHtmlElement.GetHtmlElement(), Is.EqualTo("<b><a href=\"www.google.com\">Google</a></b>"));
            _htmlElement.Verify(htmlElement => htmlElement.GetHtmlElement(), Times.Once);
        }

        [Test]
        public void TestGetHtmlElementShouldReturnHyperLinkifiedItalicizedAndBoldenedElement()
        {
            var italicizedLinkifiedElement = new ItalicizeHtmlElement(_hyperLinkifyHtmlElement);
            var boldenedItalicizedLinkifiedHtmlElement = new BoldenHtmlElement(italicizedLinkifiedElement);
            Assert.That(boldenedItalicizedLinkifiedHtmlElement.GetHtmlElement(), Is.EqualTo("<b><em><a href=\"www.google.com\">Google</a></em></b>"));
            _htmlElement.Verify(htmlElement => htmlElement.GetHtmlElement(), Times.Once);
        }
    }
}