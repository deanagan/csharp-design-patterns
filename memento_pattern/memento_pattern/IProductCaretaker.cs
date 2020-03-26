namespace MementoPattern
{
    public interface IProductCaretaker
    {
        void AddProductMemento(IProduct product);
        IProduct GetLastMemento();
    }
}
