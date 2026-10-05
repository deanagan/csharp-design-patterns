using System;

namespace Decorator
{
    public class ItalicizeHtmlElement : HtmlElementDecorator
    {
        public ItalicizeHtmlElement(IHtmlElement htmlElement) : base(htmlElement)
        {

        }

        public override string GetHtmlElement()
        {
            return $"<em>{base.GetWrappedHtmlElement()}</em>";
        }

    }
}
