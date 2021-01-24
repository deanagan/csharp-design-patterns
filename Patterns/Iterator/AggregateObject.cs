using System.Collections;
using System.Collections.Generic;

namespace Iterator
{
    public abstract class AggregateObject : IEnumerable
    {
        public abstract IEnumerator GetEnumerator();

    }
}
