namespace Decorator;

public abstract class HtmlElementDecorator(IHtmlElement htmlElement) : IHtmlElement
{
    IHtmlElement _htmlElement = htmlElement;

    public abstract string GetHtmlElement();

    protected string GetWrappedHtmlElement()
    {
        return _htmlElement.GetHtmlElement();
    }
}
