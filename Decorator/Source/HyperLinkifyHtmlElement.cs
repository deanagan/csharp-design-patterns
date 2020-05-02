using System;

namespace Decorator
{
    public class HyperLinkifyHtmlElement : HtmlElementDecorator
    {
        private string _link;
        public HyperLinkifyHtmlElement(string link, IHtmlElement htmlElement) : base(htmlElement)
        {
            _link = link;
        }

        public override string GetHtmlElement()
        {
            return $"<a href=\"{_link}\">{GetWrappedHtmlElement()}</a>";
        }
    }

}
