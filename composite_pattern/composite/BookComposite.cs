using System;
using System.Collections.Generic;
using System.Linq;

namespace Composite
{
    public class BookComposite : IBook
    {
        public decimal Price {get;}
        public string Name { get; private set; }
        
        public BookComposite(int discount, string name)
        {
        }

        public void Add(IBook book)
        {
            
        }
    }
}