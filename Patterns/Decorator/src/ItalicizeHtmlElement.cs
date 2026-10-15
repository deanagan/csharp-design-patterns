namespace Decorator;

public class ItalicizeHtmlElement(IHtmlElement htmlElement) : HtmlElementDecorator(htmlElement)
{
    public override string GetHtmlElement()
    {
        return $"<em>{base.GetWrappedHtmlElement()}</em>";
    }

}
