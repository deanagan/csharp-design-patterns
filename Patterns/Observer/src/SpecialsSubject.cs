using System.Collections.Generic;

namespace Observer
{
    public class SpecialsSubject : ISubject
    {
        public delegate void Callback(string s);
        public required string SubjectState { get; set; }

        private readonly List<IObserver> _observers = [];

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            _observers.ForEach(observer => observer.Update(this));
        }


    }
}
