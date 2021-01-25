using System.Collections;

namespace Iterator
{
    public abstract class GenericIterator : IEnumerator
    {

        object IEnumerator.Current => Current();

        public abstract object Current();

        public abstract bool MoveNext();

        public abstract void Reset();

       public abstract bool HasNext();
    }
}
