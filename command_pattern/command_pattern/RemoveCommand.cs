namespace Command
{
    public class RemoveCommand : ICommand
    {
        private IProductList _productList;
        public RemoveCommand(IProductList productList)
        {
            _productList = productList;
        }
        public void Execute(IProduct product)
        {
            _productList.Products.Remove(product);
        }
    }
}