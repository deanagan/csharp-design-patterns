using System;

namespace Composite
{
    public class Book : IBook
    {
        public decimal Price { get; private set; }
        public string Name { get; private set; }

        public Book(string name, decimal price)
        {
            
        }
    }
}