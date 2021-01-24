using System;
using System.Collections;
using System.Collections.Generic;

namespace Iterator
{
    public abstract class GenericIterator : IEnumerator
    {

        object IEnumerator.Current => Current();

        // Returns the current element
        public abstract object Current();

        // Move forward to next element
        public abstract bool MoveNext();

        // Rewinds the Iterator to the first element
        public abstract void Reset();


        public abstract bool HasNext();
    }
}
