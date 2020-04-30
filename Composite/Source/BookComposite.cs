using System;
using System.Collections.Generic;
using System.Linq;

namespace Composite
{
    public class BookComposite : IBook
    {
        private List<IBook> _books;
        public decimal Price => _books.Select(book => book.Price).Sum();
        public string Name { get; private set; }
        public int Discount {get; private set;}
        public BookComposite(int discount, string name)
        {
            Discount = discount;
            Name = name;
            _books = new List<IBook>();
        }

        public void Add(IBook book)
        {
            _books.Add(book);
        }
    }
}