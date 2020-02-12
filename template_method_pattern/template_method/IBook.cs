using System;

namespace TemplateMethod
{
    public interface IBook
    {
        decimal Price { get; }
        string Name { get; }
        int Discount { get; }
    }
}
