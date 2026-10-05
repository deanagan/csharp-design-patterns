namespace Memento
{
    public interface IProductCaretaker
    {
        void AddProductMemento(Product product);
        Product GetLastMemento();
    }
}
