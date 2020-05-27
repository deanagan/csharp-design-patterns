namespace Interpreter
{
    public interface IProduct
    {
        bool WillExpire { get; set; }
        string Name { get; set; }
        decimal Price { get; set; }
    }
}
