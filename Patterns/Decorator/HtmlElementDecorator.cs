using System;

namespace Decorator
{
    public abstract class HtmlElementDecorator : IHtmlElement
    {
        IHtmlElement _htmlElement;
        public HtmlElementDecorator(IHtmlElement htmlElement) => _htmlElement = htmlElement;

        public abstract string GetHtmlElement();

        protected string GetWrappedHtmlElement()
        {
            return _htmlElement.GetHtmlElement();
        }
    }
}
