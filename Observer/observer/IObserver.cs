namespace observer
{
    public interface IObserver
    {
        string Update(ISubject subject);
    }
}