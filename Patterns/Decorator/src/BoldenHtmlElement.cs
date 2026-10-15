namespace Decorator;

public class BoldenHtmlElement(IHtmlElement htmlElement) : HtmlElementDecorator(htmlElement)
{
    public override string GetHtmlElement()
    {
        return $"<b>{base.GetWrappedHtmlElement()}</b>";
    }
}
