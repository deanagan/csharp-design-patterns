namespace Decorator;

public class HyperLinkifyHtmlElement(string link, IHtmlElement htmlElement) : HtmlElementDecorator(htmlElement)
{
    public override string GetHtmlElement()
    {
        return $"<a href=\"{link}\">{base.GetWrappedHtmlElement()}</a>";
    }
}
