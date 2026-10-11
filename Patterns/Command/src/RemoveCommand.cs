namespace Command;

public class RemoveCommand(IProductList productList, IProduct product) : ICommand
{
    public void Execute()
    {
        productList.Products.Remove(product);
    }
}
