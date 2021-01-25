using System.Collections;

namespace Iterator
{
    public abstract class AggregateObject : IEnumerable
    {
        public abstract IEnumerator GetEnumerator();
    }
}
