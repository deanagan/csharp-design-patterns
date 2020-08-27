using System.Collections.Generic;

namespace Observer
{
    public class SpecialsSubject : ISubject
    {
        public delegate void Callback (string s);
        public string SubjectState {get; set;}

        private List<IObserver> _observers = new List<IObserver>();

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
