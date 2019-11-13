using System;

namespace decorator_lib
{
    public class ItalicizeHtmlElement : HtmlElementDecorator
    {
        public ItalicizeHtmlElement(IHtmlElement htmlElement) : base(htmlElement)
        {

        }

        public override string GetHtmlElement()
        {
            return $"<em>{GetWrappedHtmlElement()}</em>";
        }

    }
}