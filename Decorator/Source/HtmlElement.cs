using System;


namespace Decorator
{
    public class HtmlElement : IHtmlElement
    {
        private readonly string _value;
        HtmlElement(string value)
        {
            _value = value;
        }

        public string GetHtmlElement()
        {
            return _value;
        }
    }
}
