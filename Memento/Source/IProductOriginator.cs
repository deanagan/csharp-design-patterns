namespace Memento
{
    public interface IProductOriginator
    {
        void SetMemento(Product product);
        Product GetMemento();
    }
}
