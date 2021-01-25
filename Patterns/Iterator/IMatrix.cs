namespace Iterator
{
    public interface IMatrix<T>
    {
        T this[int row, int column] {get;set;}
    }
}
