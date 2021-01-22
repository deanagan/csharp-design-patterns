using System;

namespace Iterator
{
    public abstract class GenericIterator : IEnumerator
    {
        AggregateObject IEnumerator.Current => Current();

        public abstract AggregateObject Current();
        public abstract bool HasNext();
        public abstract void Next();
    }
}
