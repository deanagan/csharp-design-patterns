namespace Command;

public class ClearCommand(IProductList productList) : ICommand
{
    public void Execute()
    {
        productList.Products.Clear();
    }
}
