namespace MementoPattern
{
    public interface IProductOriginator
    {
        void SetMemento(IProduct product);
        IProduct CreateMemento();
    }
}
