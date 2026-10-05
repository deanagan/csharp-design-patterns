using System;

namespace Composite
{
    public class Book : IBook
    {
        public decimal Price { get; private set; }
        public string Name { get; private set; }

        public int Discount {get; private set;}

        public Book(string name, decimal price, int discount = 0)
        {
            Price = price;
            Name = name;
            Discount = discount;
        }
    }
}