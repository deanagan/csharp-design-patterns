using System;

namespace decorator_lib
{
    public class BoldenHtmlElement : HtmlElementDecorator
    {
        public BoldenHtmlElement(IHtmlElement htmlElement) : base(htmlElement)
        {
        }

        public override string GetHtmlElement()
        {
            return $"<b>{GetWrappedHtmlElement()}</b>";
        }
    }
}
