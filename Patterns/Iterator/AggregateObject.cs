using System;

namespace Iterator
{
    abstract class AggregateObject : IEnumerable
    {
        public abstract IEnumerator GetEnumerator();
    }
}
