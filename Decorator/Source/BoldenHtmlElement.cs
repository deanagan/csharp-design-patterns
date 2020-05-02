using System;

namespace Decorator
{
    public class BoldenHtmlElement : HtmlElementDecorator
    {
        public BoldenHtmlElement(IHtmlElement htmlElement) : base(htmlElement)
        {
        }

        public override string GetHtmlElement()
        {
            return $"<b>{base.GetWrappedHtmlElement()}</b>";
        }
    }
}
