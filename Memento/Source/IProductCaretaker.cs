namespace MementoPattern
{
    public interface IProductCaretaker
    {
        void AddProductMemento(Product product);
        Product GetLastMemento();
    }
}
