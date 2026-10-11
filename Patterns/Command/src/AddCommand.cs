namespace Command;

public class AddCommand(IProductList productList, IProduct product) : ICommand
{
    public void Execute()
    {
        productList.Products.Add(product);
    }
}
