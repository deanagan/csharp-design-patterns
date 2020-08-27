namespace Observer
{
    public interface IObserver
    {
        string Update(ISubject subject);
    }
}
