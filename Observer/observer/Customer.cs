using System;

namespace observer
{
    public class Customer : IObserver
    {
        public string Update(ISubject subject)
        {
            return $"Customer received {subject.SubjectState}";
        }
    }
}